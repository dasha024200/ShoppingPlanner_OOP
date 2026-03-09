using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Strategy
{
    public class SortByName : ISortStrategy
    {
        public List<Item> Sort(List<Item> items)
        {
            return items.OrderBy(item => item.Name).ToList();
        }
    }
}