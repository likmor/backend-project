using Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using WebApi.Configuration;

namespace WebApi;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddIdentity<UserEntity, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddSingleton<JwtSettings>();
        builder.Services.ConfigureJWT(new JwtSettings(builder.Configuration));
        builder.Services.ConfigureCors();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}