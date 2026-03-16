using ShoppingPlanner_OOP.Interfaces;
namespace ShoppingPlanner_OOP.Services;
public class BudgetObserver : IShoppingListObserver
{
private readonly BudgetService _budgetService;
public BudgetObserver(BudgetService budgetService)
{
_budgetService = budgetService;
}
public void Update(decimal total)
{
_budgetService.UpdateTotal(total);
}
}