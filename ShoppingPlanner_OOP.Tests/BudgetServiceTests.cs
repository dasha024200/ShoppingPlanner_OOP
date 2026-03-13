using ShoppingPlanner_OOP.Services;
using Xunit;

namespace ShoppingPlanner_OOP.Tests;

public class BudgetServiceTests
{
    [Fact]
    public void SetBudget_And_UpdateTotal_Should_Calculate_RemainingBudget()
    {
        var service = new BudgetService();

        service.SetBudget(500);
        service.UpdateTotal(200);

        Assert.Equal(300, service.GetRemainingBudget());
    }

    [Fact]
    public void IsOverLimit_Should_Return_True_When_Total_Exceeds_Budget()
    {
        var service = new BudgetService();

        service.SetBudget(300);
        service.UpdateTotal(450);

        Assert.True(service.IsOverLimit());
    }

    [Fact]
    public void IsOverLimit_Should_Return_False_When_Total_Is_Within_Budget()
    {
        var service = new BudgetService();

        service.SetBudget(300);
        service.UpdateTotal(250);

        Assert.False(service.IsOverLimit());
    }
}