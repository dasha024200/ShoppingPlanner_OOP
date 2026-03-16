namespace ShoppingPlanner_OOP.Models.Entities;

public class ShoppingItemEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = "Food";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid ShoppingListId { get; set; }
    public ShoppingListEntity? ShoppingList { get; set; }
}