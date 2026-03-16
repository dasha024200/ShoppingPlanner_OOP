using ShoppingPlanner_OOP.Interfaces;
using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Tests;

public class ShoppingListTests
{
    [Fact]
    public void AddItem_AddsItemToList()
    {
        var list = new ShoppingList("Test");
        var item = new FoodItem("Milk", 40, new Category("Food"));

        list.AddItem(item);

        Assert.Single(list.GetItems());
    }

    [Fact]
    public void RemoveItem_WhenItemExists_ReturnsTrue()
    {
        var list = new ShoppingList("Test");
        var item = new FoodItem("Milk", 40, new Category("Food"));
        list.AddItem(item);

        var result = list.RemoveItem(item.Id);

        Assert.True(result);
    }

    [Fact]
    public void RemoveItem_WhenItemDoesNotExist_ReturnsFalse()
    {
        var list = new ShoppingList("Test");

        var result = list.RemoveItem(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public void GetTotal_ReturnsCorrectSum()
    {
        var list = new ShoppingList("Test");
        list.AddItem(new FoodItem("Milk", 40, new Category("Food")));
        list.AddItem(new HouseholdItem("Soap", 20, new Category("Household")));

        var result = list.GetTotal();

        Assert.Equal(60, result);
    }

    [Fact]
    public void AddObserver_AndAddItem_NotifiesObserver()
    {
        var list = new ShoppingList("Test");
        var observer = new FakeObserver();
        list.AddObserver(observer);

        list.AddItem(new FoodItem("Milk", 40, new Category("Food")));

        Assert.Equal(40, observer.LastTotal);
    }
}

internal class FakeObserver : IShoppingListObserver
{
    public decimal LastTotal { get; private set; }

    public void Update(decimal total)
    {
        LastTotal = total;
    }
}