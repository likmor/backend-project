using Infrastructure.Services;
using Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        var conString = builder.Configuration.GetConnectionString("RestaurantsDatabase");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(conString);
        });
        builder.Services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(conString);
        });
        builder.Services.AddIdentity<UserEntity, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<IRestaurantService, RestaurantService>();
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