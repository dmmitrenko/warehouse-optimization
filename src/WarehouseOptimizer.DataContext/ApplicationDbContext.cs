using Microsoft.EntityFrameworkCore;
using WarehouseOptimizer.Domain.Models;

namespace WarehouseOptimizer.DataContext;

public class ApplicationDbContext : DbContext
{
    protected ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SKURecord> SKURecords { get; set; }
    public DbSet<WarehouseCell> WarehouseCell { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SKURecord>(entity =>
        {
            entity.ToTable("SKURecords");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.SKU)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Weight)
                .HasColumnType("decimal(18,2)");
            entity.Property(e => e.Volume)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.CreationTimestamp)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.IsOutdated)
                .HasDefaultValue(false);
        });

        modelBuilder.Entity<WarehouseCell>(entity =>
        {
            entity.ToTable("WarehouseCells");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CellCode)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.MaxWeight)
                .HasColumnType("decimal(18,2)");
            entity.Property(e => e.VolumeCapacity)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.X)
                .IsRequired();
            entity.Property(e => e.Y)
                .IsRequired();
            entity.Property(e => e.Z)
                .IsRequired();

            entity.Property(e => e.IsOutdated)
                .HasDefaultValue(false);
        });

        base.OnModelCreating(modelBuilder);
    }
}
