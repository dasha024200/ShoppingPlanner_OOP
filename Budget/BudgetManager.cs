namespace ShoppingPlanner_OOP.Budget
{
    public class BudgetManager
    {
        private static BudgetManager? instance;
        private decimal budgetLimit;
        private decimal currentTotal;

        private BudgetManager()
        {
            budgetLimit = 0;
            currentTotal = 0;
        }

        public static BudgetManager Instance()
        {
            if (instance == null)
            {
                instance = new BudgetManager();
            }

            return instance;
        }

        public void SetBudget(decimal limit)
        {
            if (limit < 0)
            {
                throw new ArgumentException("Бюджет не може бути від’ємним.");
            }

            budgetLimit = limit;
        }

        public void UpdateTotal(decimal total)
        {
            currentTotal = total;
        }

        public bool IsOverLimit()
        {
            return currentTotal > budgetLimit;
        }

        public decimal GetBudgetLimit()
        {
            return budgetLimit;
        }

        public decimal GetCurrentTotal()
        {
            return currentTotal;
        }
    }
}