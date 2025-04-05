using Microsoft.EntityFrameworkCore;
using UserAlertManagement.Data.Models;

namespace UserAlertManagement.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Alert> Alerts => Set<Alert>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(u => u.LastLogin)
                .HasDefaultValueSql("NULL");
        });

        modelBuilder.Entity<Alert>(entity =>
        {
            entity.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(a => a.IsOn)
                .HasDefaultValue(true);
            
            entity.Property(a => a.IsActive)
                .HasDefaultValue(true);

            entity.HasIndex(a => a.FromAirport);
            entity.HasIndex(a => a.ToAirport);
            entity.HasIndex(a => a.MaxPrice);
            entity.HasIndex(a => a.DepartureDate);
        });
        
    }
}

