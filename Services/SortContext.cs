using ShoppingPlanner_OOP.Interfaces;
using ShoppingPlanner_OOP.Models;
namespace ShoppingPlanner_OOP.Services;
public class SortContext
{
private ISortStrategy? _strategy;
public void SetStrategy(ISortStrategy strategy)
{
_strategy = strategy;
}
public List<Item> Apply(List<Item> items)
{
if (_strategy == null)
{
return items;
}
return _strategy.Sort(items);
}
}