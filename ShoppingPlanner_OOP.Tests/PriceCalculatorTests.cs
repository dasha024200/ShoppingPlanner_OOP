using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Services;
using Xunit;

namespace ShoppingPlanner_OOP.Tests;

public class PriceCalculatorTests
{
    [Fact]
    public void CalculateTotal_Should_Return_Sum_Of_All_Item_Prices()
    {
        var calculator = new PriceCalculator();
        var category = new Category("Food");

        var items = new List<Item>
        {
            new FoodItem("Milk", 40, category),
            new FoodItem("Bread", 25, category),
            new FoodItem("Cheese", 85, category)
        };

        var total = calculator.CalculateTotal(items);

        Assert.Equal(150, total);
    }
}