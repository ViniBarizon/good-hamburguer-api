using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>(order =>
        {
            order.HasKey(o => o.Id);

            order.Navigation(o => o.Items)
                 .HasField("_items")
                 .UsePropertyAccessMode(PropertyAccessMode.Field);

            order.OwnsMany(o => o.Items, items =>
            {
                items.WithOwner();
                items.Property(i => i.Type).HasConversion<string>();
            });
        });
    }
}