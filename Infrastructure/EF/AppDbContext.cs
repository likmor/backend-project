using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class AppDbContext : IdentityDbContext<UserEntity>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var adminId = "A4757E31-C82D-44C3-BA8E-385E58FEA387";
        var createdAt = new DateTime(2025, 04, 08);
        var adminUser = new UserEntity()
        {
            Id = adminId,
            Email = "admin@wsei.edu.pl",
            NormalizedEmail = "ADMIN@WSEI.EDU.PL",
            UserName = "ADMIN",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true,
            ConcurrencyStamp = adminId,
            SecurityStamp = adminId,
            PasswordHash = "AQAAAAIAAYagAAAAEOns2uqbTvCCoZ88/fzH96Tcbw0yQD5hX6GuyUwNuLKWABjoNkCalPQRL28fXduCGg=="
        };
        builder.Entity<UserEntity>()
            .HasData(adminUser);
        builder.Entity<UserEntity>()
            .OwnsOne(u => u.Details)
            .HasData(
                new
                {
                    UserEntityId = adminId,
                    CreatedAt = createdAt
                }
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"data source=c:\data\app.db");
    }
}