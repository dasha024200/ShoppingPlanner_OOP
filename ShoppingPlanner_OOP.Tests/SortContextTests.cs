using ShoppingPlanner_OOP.Interfaces;
using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Services;

namespace ShoppingPlanner_OOP.Tests;

public class SortContextTests
{
    [Fact]
    public void Apply_WhenStrategyIsNotSet_ReturnsSameItems()
    {
        var context = new SortContext();
        var items = new List<Item>
        {
            new FoodItem("B", 20, new Category("Food")),
            new FoodItem("A", 10, new Category("Food"))
        };

        var result = context.Apply(items);

        Assert.Equal(items, result);
    }

    [Fact]
    public void Apply_WhenStrategyIsSet_UsesStrategy()
    {
        var context = new SortContext();
        context.SetStrategy(new FakeSortStrategy());

        var items = new List<Item>
        {
            new FoodItem("B", 20, new Category("Food")),
            new FoodItem("A", 10, new Category("Food"))
        };

        var result = context.Apply(items);

        Assert.Equal("A", result[0].Name);
        Assert.Equal("B", result[1].Name);
    }
}

internal class FakeSortStrategy : ISortStrategy
{
    public List<Item> Sort(List<Item> items)
    {
        return items.OrderBy(x => x.Name).ToList();
    }
}