using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Strategy;
using Xunit;

namespace ShoppingPlanner_OOP.Tests;

public class FactoryAndSortTests
{
    [Fact]
    public void DefaultItemFactory_Should_Create_FoodItem()
    {
        var factory = new DefaultItemFactory();
        var category = new Category("Food");

        var item = factory.CreateItem("Apple", 20, category);

        Assert.IsType<FoodItem>(item);
    }

    [Fact]
    public void DefaultItemFactory_Should_Create_HouseholdItem()
    {
        var factory = new DefaultItemFactory();
        var category = new Category("Household");

        var item = factory.CreateItem("Soap", 35, category);

        Assert.IsType<HouseholdItem>(item);
    }

    [Fact]
    public void DefaultItemFactory_Should_Create_ElectronicsItem()
    {
        var factory = new DefaultItemFactory();
        var category = new Category("Electronics");

        var item = factory.CreateItem("Mouse", 500, category);

        Assert.IsType<ElectronicsItem>(item);
    }

    [Fact]
    public void SortByName_Should_Sort_Items_By_Name()
    {
        var strategy = new SortByName();
        var items = new List<Item>
        {
            new FoodItem("Milk", 50, new Category("Food")),
            new FoodItem("Apple", 20, new Category("Food")),
            new FoodItem("Bread", 30, new Category("Food"))
        };

        var sorted = strategy.Sort(items);

        Assert.Equal("Apple", sorted[0].Name);
        Assert.Equal("Bread", sorted[1].Name);
        Assert.Equal("Milk", sorted[2].Name);
    }

    [Fact]
    public void SortByPrice_Should_Sort_Items_By_Price()
    {
        var strategy = new SortByPrice();
        var items = new List<Item>
        {
            new FoodItem("Milk", 50, new Category("Food")),
            new FoodItem("Apple", 20, new Category("Food")),
            new FoodItem("Bread", 30, new Category("Food"))
        };

        var sorted = strategy.Sort(items);

        Assert.Equal(20, sorted[0].Price);
        Assert.Equal(30, sorted[1].Price);
        Assert.Equal(50, sorted[2].Price);
    }

    [Fact]
    public void SortByCategory_Should_Sort_Items_By_Category_Name()
    {
        var strategy = new SortByCategory();
        var items = new List<Item>
        {
            new FoodItem("Milk", 50, new Category("Food")),
            new HouseholdItem("Soap", 30, new Category("Household")),
            new ElectronicsItem("Mouse", 500, new Category("Electronics"))
        };

        var sorted = strategy.Sort(items);

        Assert.Equal("Electronics", sorted[0].Category.Name);
        Assert.Equal("Food", sorted[1].Category.Name);
        Assert.Equal("Household", sorted[2].Category.Name);
    }
}