using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingPlanner_OOP.Models.Entities;
using ShoppingPlanner_OOP.Models.Identity;

namespace ShoppingPlanner_OOP.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ShoppingListEntity> ShoppingLists => Set<ShoppingListEntity>();
    public DbSet<ShoppingItemEntity> ShoppingItems => Set<ShoppingItemEntity>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ShoppingListEntity>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ShoppingItemEntity>()
            .HasOne(x => x.ShoppingList)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ShoppingListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}