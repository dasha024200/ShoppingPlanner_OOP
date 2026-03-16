using ShoppingPlanner_OOP.Interfaces;
using ShoppingPlanner_OOP.Models;
namespace ShoppingPlanner_OOP.Strategy;
public class SortByPrice : ISortStrategy
{
public List<Item> Sort(List<Item> items)
{
return items.OrderBy(i => i.Price).ToList();
}
}