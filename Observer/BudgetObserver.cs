using ShoppingPlanner_OOP.Budget;

namespace ShoppingPlanner_OOP.Observer
{
    public class BudgetObserver : IShoppingListObserver
    {
        public void Update(decimal total)
        {
            BudgetManager.Instance().UpdateTotal(total);
        }
    }
}