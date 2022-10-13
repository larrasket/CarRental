using Microsoft.EntityFrameworkCore;
using Models.DataModels;

namespace Services;

public class CycleContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder.UseNpgsql(LocalReader.GetObj("ConnectionString"));
        => optionsBuilder.UseNpgsql(LocalReader.GetObj("ProductionString"));

    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.Entity<Vehicle>()
            .HasIndex(u => u.Number)
            .IsUnique();

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Fine> Fines { get; set; }
    public DbSet<Creator> Creators { get; set; }
    public DbSet<ClientUser> ClientUsers { get; set; }
}