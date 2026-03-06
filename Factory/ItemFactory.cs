using ShoppingPlanner.Models;

namespace ShoppingPlanner.Factory
{
    public abstract class ItemFactory
    {
        public abstract Item CreateItem(string name, decimal price, Category category);
    }
}