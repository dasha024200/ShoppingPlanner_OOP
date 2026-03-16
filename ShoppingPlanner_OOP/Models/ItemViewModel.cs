namespace ShoppingPlanner_OOP.Models;

public class ItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
}
