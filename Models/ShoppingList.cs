using ShoppingPlanner_OOP.Context;
using ShoppingPlanner_OOP.Observer;

namespace ShoppingPlanner_OOP.Models
{
    public class ShoppingList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        private readonly List<Item> items;
        private readonly List<IShoppingListObserver> observers;

        public ShoppingList(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            items = new List<Item>();
            observers = new List<IShoppingListObserver>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            NotifyObservers();
        }

        public bool RemoveItem(Guid itemId)
        {
            Item? item = items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                return false;
            }

            items.Remove(item);
            NotifyObservers();
            return true;
        }

        public decimal GetTotal()
        {
            return items.Sum(item => item.Price);
        }

        public void AddObserver(IShoppingListObserver observer)
        {
            observers.Add(observer);
        }

        public List<Item> GetItems()
        {
            return new List<Item>(items);
        }

        public List<Item> SortItems(SortContext context)
        {
            return context.Apply(GetItems());
        }

        private void NotifyObservers()
        {
            decimal total = GetTotal();

            foreach (IShoppingListObserver observer in observers)
            {
                observer.Update(total);
            }
        }
    }
}