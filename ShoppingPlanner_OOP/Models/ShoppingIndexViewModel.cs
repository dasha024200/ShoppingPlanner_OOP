namespace ShoppingPlanner_OOP.Models;

public class ShoppingIndexViewModel
{
    public string Title { get; set; } = string.Empty;
    public List<ItemViewModel> Items { get; set; } = new();
    public decimal BudgetLimit { get; set; }
    public decimal CurrentTotal { get; set; }
    public decimal RemainingBudget { get; set; }
    public bool IsOverLimit { get; set; }
    public AddItemViewModel AddItem { get; set; } = new();
}
