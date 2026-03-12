using Microsoft.AspNetCore.Mvc;
using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Services;
using ShoppingPlanner_OOP.Strategy;

namespace ShoppingPlanner_OOP.Controllers;

public class ShoppingController : Controller
{
    private readonly ShoppingSessionService _shoppingSessionService;
    private readonly BudgetService _budgetService;
    private readonly BudgetObserver _budgetObserver;
    private readonly DefaultItemFactory _itemFactory;
    private readonly SortContext _sortContext;
    private readonly ShoppingListJsonService _jsonService;
    private readonly CsvExportService _csvExportService;

    public ShoppingController(
        ShoppingSessionService shoppingSessionService,
        BudgetService budgetService,
        BudgetObserver budgetObserver,
        DefaultItemFactory itemFactory,
        SortContext sortContext,
        ShoppingListJsonService jsonService,
        CsvExportService csvExportService)
    {
        _shoppingSessionService = shoppingSessionService;
        _budgetService = budgetService;
        _budgetObserver = budgetObserver;
        _itemFactory = itemFactory;
        _sortContext = sortContext;
        _jsonService = jsonService;
        _csvExportService = csvExportService;
    }

    [HttpGet]
    public IActionResult Index(string? sortBy = null)
    {
        var shoppingList = _shoppingSessionService.GetShoppingList();

        shoppingList.AddObserver(_budgetObserver);

        decimal savedBudget = _shoppingSessionService.GetBudgetLimit();
        _budgetService.SetBudget(savedBudget);
        _budgetService.UpdateTotal(shoppingList.GetTotal());

        var items = shoppingList.GetItems();

        switch (sortBy)
        {
            case "name":
                _sortContext.SetStrategy(new SortByName());
                items = _sortContext.Apply(items);
                break;

            case "price":
                _sortContext.SetStrategy(new SortByPrice());
                items = _sortContext.Apply(items);
                break;

            case "category":
                _sortContext.SetStrategy(new SortByCategory());
                items = _sortContext.Apply(items);
                break;
        }

        var model = new ShoppingIndexViewModel
        {
            Title = shoppingList.Title,
            Items = items,
            BudgetLimit = _budgetService.GetBudgetLimit(),
            CurrentTotal = _budgetService.GetCurrentTotal(),
            RemainingBudget = _budgetService.GetRemainingBudget(),
            IsOverLimit = _budgetService.IsOverLimit(),
            AddItem = new AddItemViewModel()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetBudget(string budgetLimit)
    {
        if (!decimal.TryParse(budgetLimit, out decimal limit))
        {
            TempData["Message"] = "Неправильний формат бюджету.";
            return RedirectToAction(nameof(Index));
        }

        _budgetService.SetBudget(limit);
        _shoppingSessionService.SetBudgetLimit(limit);

        TempData["Message"] = "Бюджет збережено.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddItem([Bind(Prefix = "AddItem")] AddItemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = "Перевір правильність введених даних.";
            return RedirectToAction(nameof(Index));
        }

        var shoppingList = _shoppingSessionService.GetShoppingList();

        shoppingList.AddObserver(_budgetObserver);

        decimal savedBudget = _shoppingSessionService.GetBudgetLimit();
        _budgetService.SetBudget(savedBudget);

        var category = new Category(model.CategoryName);
        Item item = _itemFactory.CreateItem(model.Name, model.Price, category);

        shoppingList.AddItem(item);

        _shoppingSessionService.SaveShoppingList(shoppingList);

        TempData["Message"] = "Товар успішно додано.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveItem(Guid id)
    {
        var shoppingList = _shoppingSessionService.GetShoppingList();

        shoppingList.AddObserver(_budgetObserver);

        decimal savedBudget = _shoppingSessionService.GetBudgetLimit();
        _budgetService.SetBudget(savedBudget);

        shoppingList.RemoveItem(id);
        _shoppingSessionService.SaveShoppingList(shoppingList);

        TempData["Message"] = "Товар видалено.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveJson()
    {
        var shoppingList = _shoppingSessionService.GetShoppingList();

        var path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
        Directory.CreateDirectory(path);

        var filePath = Path.Combine(path, "shoppinglist.json");
        _jsonService.Save(filePath, shoppingList);

        TempData["Message"] = "Список збережено у JSON.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LoadJson()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "shoppinglist.json");
        var loaded = _jsonService.Load(path);

        if (loaded != null)
        {
            loaded.AddObserver(_budgetObserver);
            _budgetService.SetBudget(_shoppingSessionService.GetBudgetLimit());
            _budgetService.UpdateTotal(loaded.GetTotal());
            _shoppingSessionService.SaveShoppingList(loaded);

            TempData["Message"] = "Список завантажено з JSON.";
        }
        else
        {
            TempData["Message"] = "JSON-файл не знайдено або він порожній.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ExportCsv()
    {
        var shoppingList = _shoppingSessionService.GetShoppingList();
        var bytes = _csvExportService.Export(shoppingList.GetItems());

        return File(bytes, "text/csv", "shoppinglist.csv");
    }
}