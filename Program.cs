using ShoppingPlanner_OOP.Budget;
using ShoppingPlanner_OOP.Context;
using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Observer;
using ShoppingPlanner_OOP.Strategy;

namespace ShoppingPlanner_OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Category food = new Category("Продукти");
            Category tech = new Category("Техніка");
            Category home = new Category("Для дому");

            ItemFactory factory = new DefaultItemFactory();

            ShoppingList shoppingList = new ShoppingList("Мій список покупок");

            IShoppingListObserver budgetObserver = new BudgetObserver();
            shoppingList.AddObserver(budgetObserver);

            BudgetManager.Instance().SetBudget(300);

            Item item1 = factory.CreateItem("Молоко", 45.50m, food);
            Item item2 = factory.CreateItem("Навушники", 199.99m, tech);
            Item item3 = factory.CreateItem("Пральний порошок", 120.00m, home);
            Item item4 = factory.CreateItem("Хліб", 28.00m, food);

            shoppingList.AddItem(item1);
            shoppingList.AddItem(item2);
            shoppingList.AddItem(item3);
            shoppingList.AddItem(item4);

            Console.WriteLine("\nУсі товари:");
            PrintItems(shoppingList.GetItems());

            Console.WriteLine($"\nЗагальна сума: {shoppingList.GetTotal()} грн");

            SortContext sortContext = new SortContext();

            sortContext.SetStrategy(new SortByPrice());
            Console.WriteLine("\nСортування за ціною:");
            PrintItems(shoppingList.SortItems(sortContext));

            sortContext.SetStrategy(new SortByName());
            Console.WriteLine("\nСортування за назвою:");
            PrintItems(shoppingList.SortItems(sortContext));

            sortContext.SetStrategy(new SortByCategory());
            Console.WriteLine("\nСортування за категорією:");
            PrintItems(shoppingList.SortItems(sortContext));

            Console.WriteLine("\nВидаляємо товар 'Навушники'...");
            shoppingList.RemoveItem(item2.Id);

            Console.WriteLine("\nСписок після видалення:");
            PrintItems(shoppingList.GetItems());

            Console.WriteLine($"\nПідсумкова сума: {shoppingList.GetTotal()} грн");
        }

        static void PrintItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}