using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Factory
{
    public class DefaultItemFactory : ItemFactory
    {
        public override Item CreateItem(string name, decimal price, Category category)
        {
            return new Item(name, price, category);
        }
    }
}