using ShoppingPlanner_OOP.Models.Identity;

namespace ShoppingPlanner_OOP.Models.Entities;

public class ShoppingListEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "Мій список покупок";
    public decimal BudgetLimit { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    public List<ShoppingItemEntity> Items { get; set; } = new();
}