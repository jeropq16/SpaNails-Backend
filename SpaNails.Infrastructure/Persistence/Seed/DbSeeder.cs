using System;
using System.Linq;
using System.Threading.Tasks;
using SpaNails.Domain.Enums;
using SpaNails.Domain.Models;

namespace SpaNails.Infrastructure.Persistence.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Seed Users
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "SuperUser",
                    Email = "admin@spanails.com",
                    PasswordHash = "$2a$11$zXlTFShCgP7RPpucWznpsu4FtIYQbV687XZ67Xn9VlkkrSwsYmol.", // BCrypt "admin123"
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                };

                var manicurist = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.manicurist@spanails.com",
                    PasswordHash = "$2a$11$zXlTFShCgP7RPpucWznpsu4FtIYQbV687XZ67Xn9VlkkrSwsYmol.",
                    Role = UserRole.Manicurist,
                    CreatedAt = DateTime.UtcNow
                };

                var client = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.client@spanails.com",
                    PasswordHash = "$2a$11$zXlTFShCgP7RPpucWznpsu4FtIYQbV687XZ67Xn9VlkkrSwsYmol.",
                    Role = UserRole.Client,
                    CreatedAt = DateTime.UtcNow
                };

                await context.Users.AddRangeAsync(admin, manicurist, client);
                await context.SaveChangesAsync();
                
                // Seed Services
                if (!context.Services.Any())
                {
                    var service1 = new Service
                    {
                        Id = Guid.NewGuid(),
                        Name = "Manicura Clásica",
                        Description = "Limpieza, corte, limado y esmaltado tradicional.",
                        Price = 15.00m,
                        DurationInMinutes = 45,
                        IsActive = true
                    };

                    var service2 = new Service
                    {
                        Id = Guid.NewGuid(),
                        Name = "Manicura Semipermanente",
                        Description = "Esmaltado de larga duración con curado en lámpara UV.",
                        Price = 25.00m,
                        DurationInMinutes = 60,
                        IsActive = true
                    };

                    await context.Services.AddRangeAsync(service1, service2);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
