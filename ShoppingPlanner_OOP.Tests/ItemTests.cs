using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Tests;

public class ItemTests
{
    [Fact]
    public void ChangePrice_WithPositiveValue_ChangesPrice()
    {
        var item = new FoodItem("Bread", 30, new Category("Food"));

        item.ChangePrice(45);

        Assert.Equal(45, item.Price);
    }

    [Fact]
    public void ChangePrice_WithNegativeValue_DoesNotChangePrice()
    {
        var item = new FoodItem("Bread", 30, new Category("Food"));

        item.ChangePrice(-10);

        Assert.Equal(30, item.Price);
    }

    [Fact]
    public void AssignCategory_ChangesCategory()
    {
        var item = new FoodItem("Bread", 30, new Category("Food"));
        var newCategory = new Category("Household");

        item.AssignCategory(newCategory);

        Assert.Equal("Household", item.Category.Name);
    }
}