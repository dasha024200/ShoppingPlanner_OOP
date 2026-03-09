using ShoppingPlanner_OOP.Budget;

namespace ShoppingPlanner_OOP.Observer
{
    public class BudgetObserver : IShoppingListObserver
    {
        public void Update(decimal total)
        {
            BudgetManager manager = BudgetManager.Instance();
            manager.UpdateTotal(total);

            Console.WriteLine($"\nОновлена загальна сума: {total} грн");

            if (manager.IsOverLimit())
            {
                Console.WriteLine("Увага! Бюджет перевищено!");
            }
            else
            {
                Console.WriteLine("Бюджет не перевищено.");
            }
        }
    }
}