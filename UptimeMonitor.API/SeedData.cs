using Microsoft.AspNetCore.Identity;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.Entities;

namespace UptimeMonitor.API
{
    public static class SeedData
    {
        public static void EnsureSeeded(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Users.Any(u => u.Username == "admin"))
            {
                var user = new User
                {
                    Username = "admin",
                    Role = "Admin",
                    Status = true
                };

                var hasher = new PasswordHasher<User>();
                user.PasswordHash = hasher.HashPassword(user, "admin123");

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}