using ShoppingPlanner_OOP.Services;

namespace ShoppingPlanner_OOP.Tests;

public class BudgetServiceTests
{
    [Fact]
    public void SetBudget_WithPositiveValue_SetsBudget()
    {
        var service = new BudgetService();

        service.SetBudget(500);

        Assert.Equal(500, service.GetBudgetLimit());
    }

    [Fact]
    public void SetBudget_WithNegativeValue_DoesNotChangeBudget()
    {
        var service = new BudgetService();
        service.SetBudget(300);

        service.SetBudget(-100);

        Assert.Equal(300, service.GetBudgetLimit());
    }

    [Fact]
    public void UpdateTotal_AndGetRemainingBudget_ReturnsCorrectDifference()
    {
        var service = new BudgetService();
        service.SetBudget(1000);
        service.UpdateTotal(250);

        var result = service.GetRemainingBudget();

        Assert.Equal(750, result);
    }

    [Fact]
    public void IsOverLimit_WhenTotalGreaterThanBudget_ReturnsTrue()
    {
        var service = new BudgetService();
        service.SetBudget(200);
        service.UpdateTotal(250);

        var result = service.IsOverLimit();

        Assert.True(result);
    }

    [Fact]
    public void IsOverLimit_WhenTotalEqualsBudget_ReturnsFalse()
    {
        var service = new BudgetService();
        service.SetBudget(200);
        service.UpdateTotal(200);

        var result = service.IsOverLimit();

        Assert.False(result);
    }

    [Fact]
    public void GetCurrentTotal_ReturnsUpdatedValue()
    {
        var service = new BudgetService();
        service.UpdateTotal(123.45m);

        var result = service.GetCurrentTotal();

        Assert.Equal(123.45m, result);
    }
}