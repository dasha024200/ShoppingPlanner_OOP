using ShoppingPlanner_OOP.Models;
namespace ShoppingPlanner_OOP.Services;
public class PriceCalculator
{
public decimal CalculateTotal(List<Item> items)
{
return items.Sum(i => i.Price);
}
}