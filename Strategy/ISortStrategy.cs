using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Strategy
{
    public interface ISortStrategy
    {
        List<Item> Sort(List<Item> items);
    }
}