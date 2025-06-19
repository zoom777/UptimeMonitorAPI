using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Entities;

namespace UptimeMonitor.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ServiceSystem> ServiceSystems => Set<ServiceSystem>();
        public DbSet<Component> Components => Set<Component>();
        public DbSet<UptimeCheck> UptimeChecks => Set<UptimeCheck>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceSystem>()
                .HasMany(s => s.Components)
                .WithOne(c => c.ServiceSystem)
                .HasForeignKey(c => c.ServiceSystemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Component>()
                .HasMany<UptimeCheck>()
                .WithOne(c => c.Component)
                .HasForeignKey(c => c.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceSystem>()
                .HasMany<UptimeCheck>()
                .WithOne(c => c.ServiceSystem)
                .HasForeignKey(c => c.ServiceSystemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}