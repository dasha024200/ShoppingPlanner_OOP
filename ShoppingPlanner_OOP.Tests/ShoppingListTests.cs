using ShoppingPlanner_OOP.Models;
using Xunit;

namespace ShoppingPlanner_OOP.Tests;

public class ShoppingListTests
{
    [Fact]
    public void AddItem_Should_Add_Item_To_List()
    {
        var list = new ShoppingList("Test List");
        var item = new FoodItem("Milk", 50, new Category("Food"));

        list.AddItem(item);

        Assert.Single(list.GetItems());
        Assert.Equal("Milk", list.GetItems()[0].Name);
    }

    [Fact]
    public void RemoveItem_Should_Remove_Existing_Item()
    {
        var list = new ShoppingList("Test List");
        var item = new FoodItem("Milk", 50, new Category("Food"));

        list.AddItem(item);

        var result = list.RemoveItem(item.Id);

        Assert.True(result);
        Assert.Empty(list.GetItems());
    }

    [Fact]
    public void RemoveItem_Should_Return_False_When_Item_Not_Found()
    {
        var list = new ShoppingList("Test List");

        var result = list.RemoveItem(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public void GetTotal_Should_Return_Total_Price_Of_Items()
    {
        var list = new ShoppingList("Test List");

        list.AddItem(new FoodItem("Milk", 50, new Category("Food")));
        list.AddItem(new HouseholdItem("Soap", 30, new Category("Household")));

        Assert.Equal(80, list.GetTotal());
    }
}