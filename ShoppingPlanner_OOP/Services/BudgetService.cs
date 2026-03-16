namespace ShoppingPlanner_OOP.Services;
public class BudgetService
{
private decimal _budgetLimit;
private decimal _currentTotal;
public void SetBudget(decimal limit)
{
if (limit >= 0)
{
_budgetLimit = limit;
}
}
public void UpdateTotal(decimal total)
{
_currentTotal = total;
}
public decimal GetRemainingBudget()
{
return _budgetLimit- _currentTotal;
}
public bool IsOverLimit()
{
return _currentTotal > _budgetLimit;
}
public decimal GetBudgetLimit()
{
return _budgetLimit;
}
public decimal GetCurrentTotal()
{
return _currentTotal;
}
}