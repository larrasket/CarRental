using Microsoft.EntityFrameworkCore;
using Models.DataModels;

namespace Services;

public class CycleContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(LocalReader.GetObj("ConnectionString"));

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Vehicle>()
            .HasIndex(u => u.Number)
            .IsUnique();
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<HGS> HGSs { get; set; }
    public DbSet<Fine> Fines { get; set; }
    // public DbSet<User> Users { get; set; }
    public DbSet<Bill> Bills { get; set; }
}