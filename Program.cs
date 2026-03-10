using ShoppingPlanner_OOP.Budget;
using ShoppingPlanner_OOP.Context;
using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Observer;
using ShoppingPlanner_OOP.Services;
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

            shoppingList.AddItem(factory.CreateItem("Молоко", 45.50m, food));
            shoppingList.AddItem(factory.CreateItem("Навушники", 199.99m, tech));
            shoppingList.AddItem(factory.CreateItem("Пральний порошок", 120.00m, home));
            shoppingList.AddItem(factory.CreateItem("Хліб", 28.00m, food));

            Console.WriteLine("Усі товари:");
            PrintItems(shoppingList.GetItems());

            Console.WriteLine($"\nЗагальна сума: {shoppingList.GetTotal()} грн");
            Console.WriteLine($"Перевищено бюджет: {BudgetManager.Instance().IsOverLimit()}");

            SortContext sortContext = new SortContext();

            sortContext.SetStrategy(new SortByPrice());
            Console.WriteLine("\nСортування за ціною:");
            PrintItems(shoppingList.SortItems(sortContext));

            ShoppingListJsonService jsonService = new ShoppingListJsonService();
            string filePath = "shoppinglist.json";

            jsonService.SaveToJson(shoppingList, filePath);
            Console.WriteLine($"\nСписок збережено у файл: {filePath}");

            ShoppingList loadedList = jsonService.LoadFromJson(filePath);

            Console.WriteLine("\nСписок після завантаження з JSON:");
            PrintItems(loadedList.GetItems());
            Console.WriteLine($"\nСума завантаженого списку: {loadedList.GetTotal()} грн");
        }

        static void PrintItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                Console.WriteLine($"{item.Name} | {item.Category.Name} | {item.Price} грн");
            }
        }
    }
}