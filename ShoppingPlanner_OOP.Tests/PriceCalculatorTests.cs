using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Services;

namespace ShoppingPlanner_OOP.Tests;

public class PriceCalculatorTests
{
    [Fact]
    public void CalculateTotal_WithSeveralItems_ReturnsCorrectSum()
    {
        var calculator = new PriceCalculator();
        var items = new List<Item>
        {
            new FoodItem("Milk", 40, new Category("Food")),
            new HouseholdItem("Soap", 25, new Category("Household")),
            new ElectronicsItem("Mouse", 300, new Category("Electronics"))
        };

        var result = calculator.CalculateTotal(items);

        Assert.Equal(365, result);
    }

    [Fact]
    public void CalculateTotal_WithEmptyList_ReturnsZero()
    {
        var calculator = new PriceCalculator();
        var items = new List<Item>();

        var result = calculator.CalculateTotal(items);

        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateTotal_WithOneItem_ReturnsItemPrice()
    {
        var calculator = new PriceCalculator();
        var items = new List<Item>
        {
            new FoodItem("Bread", 35, new Category("Food"))
        };

        var result = calculator.CalculateTotal(items);

        Assert.Equal(35, result);
    }
}