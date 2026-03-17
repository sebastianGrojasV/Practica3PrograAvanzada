using Microsoft.EntityFrameworkCore;
using SistemaGestorAutomoviles.Domain.Entities;

namespace SistemaGestorAutomoviles.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles");
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Brand).IsRequired().HasMaxLength(60);
            entity.Property(v => v.Model).IsRequired().HasMaxLength(60);
            entity.Property(v => v.Color).IsRequired().HasMaxLength(40);
            entity.Property(v => v.Type).IsRequired().HasMaxLength(40);
            entity.Property(v => v.Transmission).IsRequired().HasMaxLength(30);
            entity.Property(v => v.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                Price = 18500,
                Color = "Blanco",
                Type = "Sedan",
                Transmission = "Automatica",
                Mileage = 35000,
                IsAvailable = true,
                RegistrationDate = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Vehicle
            {
                Id = 2,
                Brand = "Honda",
                Model = "CR-V",
                Year = 2022,
                Price = 27900,
                Color = "Gris",
                Type = "SUV",
                Transmission = "Automatica",
                Mileage = 21000,
                IsAvailable = true,
                RegistrationDate = new DateTime(2024, 2, 5, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
