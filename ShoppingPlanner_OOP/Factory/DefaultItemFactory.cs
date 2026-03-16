using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Factory;

public class DefaultItemFactory : ItemFactory
{
    public override Item CreateItem(string name, decimal price, Category category)
    {
        if (category.IsMatch("Food"))
            return new FoodItem(name, price, category);

        if (category.IsMatch("Household"))
            return new HouseholdItem(name, price, category);

        if (category.IsMatch("Electronics"))
            return new ElectronicsItem(name, price, category);

        return new FoodItem(name, price, category);
    }
}
