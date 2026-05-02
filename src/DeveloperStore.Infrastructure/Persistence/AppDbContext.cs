using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Sale> Sales => Set<Sale>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Customer).IsRequired();
            entity.Property(x => x.Branch).IsRequired();

            entity.HasMany(x => x.Items)
                  .WithOne()
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey("Id");

            entity.Property(x => x.ProductId).IsRequired();
        });
    }
}