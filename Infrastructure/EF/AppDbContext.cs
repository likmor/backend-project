using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=localhost;Database=restaurant;User Id=admin;Password=admin;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ratings__3214EC077D0525B0");

            entity.ToTable("ratings");

            entity.Property(e => e.ConsumerId)
                .HasMaxLength(50)
                .HasColumnName("Consumer_ID");
            entity.Property(e => e.FoodRating).HasColumnName("Food_Rating");
            entity.Property(e => e.OverallRating).HasColumnName("Overall_Rating");
            entity.Property(e => e.RestaurantId).HasColumnName("Restaurant_ID");
            entity.Property(e => e.ServiceRating).HasColumnName("Service_Rating");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ratings_restaurants");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.ToTable("restaurants");

            entity.Property(e => e.RestaurantId)
                .ValueGeneratedNever()
                .HasColumnName("Restaurant_ID");
            entity.Property(e => e.AlcoholService)
                .HasMaxLength(50)
                .HasColumnName("Alcohol_Service");
            entity.Property(e => e.Area).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Franchise).HasMaxLength(20);
            entity.Property(e => e.Latitude).HasMaxLength(50);
            entity.Property(e => e.Longitude).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Parking).HasMaxLength(50);
            entity.Property(e => e.Price).HasMaxLength(50);
            entity.Property(e => e.SmokingAllowed)
                .HasMaxLength(50)
                .HasColumnName("Smoking_Allowed");
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Zip_Code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
