using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Factory
{
    public abstract class ItemFactory
    {
        public abstract Item CreateItem(string name, decimal price, Category category);
    }
}