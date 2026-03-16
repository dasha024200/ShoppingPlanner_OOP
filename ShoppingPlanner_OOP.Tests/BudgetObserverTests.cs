using ShoppingPlanner_OOP.Services;

namespace ShoppingPlanner_OOP.Tests;

public class BudgetObserverTests
{
    [Fact]
    public void Update_SendsTotalToBudgetService()
    {
        var service = new BudgetService();
        var observer = new BudgetObserver(service);

        observer.Update(555);

        Assert.Equal(555, service.GetCurrentTotal());
    }
}