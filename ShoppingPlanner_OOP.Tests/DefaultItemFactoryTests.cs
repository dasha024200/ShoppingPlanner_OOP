using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Tests;

public class DefaultItemFactoryTests
{
    [Fact]
    public void CreateItem_ForFood_ReturnsFoodItem()
    {
        var factory = new DefaultItemFactory();

        var item = factory.CreateItem("Apple", 20, new Category("Food"));

        Assert.IsType<FoodItem>(item);
    }

    [Fact]
    public void CreateItem_ForHousehold_ReturnsHouseholdItem()
    {
        var factory = new DefaultItemFactory();

        var item = factory.CreateItem("Shampoo", 120, new Category("Household"));

        Assert.IsType<HouseholdItem>(item);
    }

    [Fact]
    public void CreateItem_ForElectronics_ReturnsElectronicsItem()
    {
        var factory = new DefaultItemFactory();

        var item = factory.CreateItem("Keyboard", 900, new Category("Electronics"));

        Assert.IsType<ElectronicsItem>(item);
    }

    [Fact]
    public void CreateItem_ForUnknownCategory_ReturnsFoodItemByDefault()
    {
        var factory = new DefaultItemFactory();

        var item = factory.CreateItem("Something", 50, new Category("Other"));

        Assert.IsType<FoodItem>(item);
    }
}