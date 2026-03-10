using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Strategy;

namespace ShoppingPlanner_OOP.Context
{
    public class SortContext
    {
        private ISortStrategy? strategy;

        public void SetStrategy(ISortStrategy strategy)
        {
            this.strategy = strategy;
        }

        public List<Item> Apply(List<Item> items)
        {
            if (strategy == null)
            {
                return items;
            }

            return strategy.Sort(items);
        }
    }
}