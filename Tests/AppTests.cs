using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Infrastructure.Dto;
using Infrastructure.EF;
using Microsoft.Extensions.DependencyInjection;
using WebApi;

namespace Tests;

public class AppTests : IClassFixture<AppTestFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly AppTestFactory<Program> _app;
    private readonly IdentityDbContext _context;

    public AppTests(AppTestFactory<Program> app)
    {
        _app = app;
        _client = _app.CreateClient();
        using (var scope = app.Services.CreateScope())
        {
            _context = scope.ServiceProvider.GetService<IdentityDbContext>();
            var adminId = "A4757E31-C82D-44C3-BA8E-385E58FEA387";
            if (!_context.Users.Any(u => u.Id == adminId))
            {
                _context.Users.Add(
                    new UserEntity()
                    {
                        Id = adminId,
                        Email = "admin@wsei.edu.pl",
                        NormalizedEmail = "ADMIN@WSEI.EDU.PL",
                        UserName = "ADMIN",
                        NormalizedUserName = "ADMIN",
                        EmailConfirmed = true,
                        ConcurrencyStamp = adminId,
                        SecurityStamp = adminId,
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEOns2uqbTvCCoZ88/fzH96Tcbw0yQD5hX6GuyUwNuLKWABjoNkCalPQRL28fXduCGg=="
                    });
                _context.SaveChanges();
            }
        }
    }

    [Fact]
    public async void ValidLoginTest()
    {
        var loginBody = new LoginDto()
        {
            Username = "admin",
            Password = "1234!"
        };
        var result = await _client.PostAsJsonAsync("/api/users/login", loginBody);
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        
        var node = JsonNode.Parse(await result.Content.ReadAsStringAsync());
        var token = node["token"].AsValue().ToString();
        Assert.NotEmpty(token);
        
        HttpRequestMessage request = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://localhost:7095/api/books"),
            Method = HttpMethod.Get,
            Headers =
            {
                {HttpRequestHeader.Authorization.ToString(), $"Bearer {token}"},
                {HttpRequestHeader.ContentType.ToString(), "application/json"}
            },
        };
        var response = await _client.SendAsync(request);
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("user", "1234!")]
    [InlineData("admin", "!1234")]
    [InlineData("admin", "12345!")]
    public async void InvalidLoginTest(string username, string password)
    {
        var loginBody = new LoginDto()
        {
            Username = username,
            Password = password
        };
        var result = await _client.PostAsJsonAsync("/api/users/login", loginBody);
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async void TestBookControllerForUnauthorized()
    {
        var response = await _client.GetAsync("/api/books");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}