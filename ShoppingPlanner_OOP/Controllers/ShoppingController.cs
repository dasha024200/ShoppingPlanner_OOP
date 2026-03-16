using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPlanner_OOP.Data;
using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Models;
using ShoppingPlanner_OOP.Models.Entities;
using ShoppingPlanner_OOP.Models.Identity;
using ShoppingPlanner_OOP.Services;

namespace ShoppingPlanner_OOP.Controllers;

[Authorize]
public class ShoppingController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BudgetService _budgetService;
    private readonly DefaultItemFactory _itemFactory;
    private readonly CsvExportService _csvExportService;

    public ShoppingController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        BudgetService budgetService,
        DefaultItemFactory itemFactory,
        CsvExportService csvExportService)
    {
        _context = context;
        _userManager = userManager;
        _budgetService = budgetService;
        _itemFactory = itemFactory;
        _csvExportService = csvExportService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? sortBy = null)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var shoppingList = await GetOrCreateShoppingListAsync(user.Id);

        var items = shoppingList.Items
            .Select(i => new ItemViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                CategoryName = i.CategoryName,
                ItemType = GetItemTypeByCategory(i.CategoryName)
            })
            .ToList();

        items = sortBy switch
        {
            "name" => items.OrderBy(i => i.Name).ToList(),
            "price" => items.OrderBy(i => i.Price).ToList(),
            "category" => items.OrderBy(i => i.CategoryName).ToList(),
            _ => items
        };

        var currentTotal = items.Sum(i => i.Price);

        _budgetService.SetBudget(shoppingList.BudgetLimit);
        _budgetService.UpdateTotal(currentTotal);

        var model = new ShoppingIndexViewModel
        {
            Title = shoppingList.Title,
            Items = items,
            BudgetLimit = shoppingList.BudgetLimit,
            CurrentTotal = currentTotal,
            RemainingBudget = _budgetService.GetRemainingBudget(),
            IsOverLimit = _budgetService.IsOverLimit(),
            AddItem = new AddItemViewModel()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetBudget(decimal budgetLimit)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        if (budgetLimit < 0)
        {
            TempData["Message"] = "Бюджет не може бути від’ємним.";
            return RedirectToAction(nameof(Index));
        }

        var shoppingList = await GetOrCreateShoppingListAsync(user.Id);
        shoppingList.BudgetLimit = budgetLimit;
        shoppingList.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        TempData["Message"] = "Бюджет збережено.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem([Bind(Prefix = "AddItem")] AddItemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = "Перевір правильність введених даних.";
            return RedirectToAction(nameof(Index));
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var shoppingList = await GetOrCreateShoppingListAsync(user.Id);

        var category = new Category(model.CategoryName);
        var item = _itemFactory.CreateItem(model.Name, model.Price, category);

        var entity = new ShoppingItemEntity
        {
            Name = item.Name,
            Price = item.Price,
            CategoryName = category.Name,
            ShoppingListId = shoppingList.Id
        };

        _context.ShoppingItems.Add(entity);
        shoppingList.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        TempData["Message"] = "Товар успішно додано.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var shoppingList = await GetOrCreateShoppingListAsync(user.Id);

        var item = await _context.ShoppingItems
            .FirstOrDefaultAsync(x => x.Id == id && x.ShoppingListId == shoppingList.Id);

        if (item == null)
        {
            TempData["Message"] = "Товар не знайдено.";
            return RedirectToAction(nameof(Index));
        }

        _context.ShoppingItems.Remove(item);
        shoppingList.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        TempData["Message"] = "Товар видалено.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExportCsv()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var shoppingList = await GetOrCreateShoppingListAsync(user.Id);

        var items = shoppingList.Items
            .Select(i => new ItemViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                CategoryName = i.CategoryName,
                ItemType = GetItemTypeByCategory(i.CategoryName)
            })
            .ToList();

        var bytes = _csvExportService.ExportFromViewModel(items);
        return File(bytes, "text/csv", "shoppinglist.csv");
    }

    [HttpGet]
    public async Task<IActionResult> History()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var history = await _context.ShoppingLists
            .Include(x => x.Items)
            .Where(x => x.UserId == user.Id && x.IsArchived)
            .OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt)
            .ToListAsync();

        return View(history);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ArchiveCurrentList()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var shoppingList = await _context.ShoppingLists
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == user.Id && !x.IsArchived);

        if (shoppingList == null)
        {
            TempData["Message"] = "Активний список не знайдено.";
            return RedirectToAction(nameof(Index));
        }

        if (!shoppingList.Items.Any())
        {
            TempData["Message"] = "Список порожній, немає що завершувати.";
            return RedirectToAction(nameof(Index));
        }

        shoppingList.IsArchived = true;
        shoppingList.UpdatedAt = DateTime.UtcNow;

        var newShoppingList = new ShoppingListEntity
        {
            Title = "Мій список покупок",
            BudgetLimit = 0,
            UserId = user.Id,
            IsArchived = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.ShoppingLists.Add(newShoppingList);

        await _context.SaveChangesAsync();

        TempData["Message"] = "Список перенесено в історію.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<ShoppingListEntity> GetOrCreateShoppingListAsync(string userId)
    {
        var shoppingList = await _context.ShoppingLists
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsArchived);

        if (shoppingList != null)
        {
            return shoppingList;
        }

        shoppingList = new ShoppingListEntity
        {
            Title = "Мій список покупок",
            BudgetLimit = 0,
            UserId = userId,
            IsArchived = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.ShoppingLists.Add(shoppingList);
        await _context.SaveChangesAsync();

        return shoppingList;
    }

    private string GetItemTypeByCategory(string categoryName)
    {
        return categoryName switch
        {
            "Food" => nameof(FoodItem),
            "Household" => nameof(HouseholdItem),
            "Electronics" => nameof(ElectronicsItem),
            _ => "Item"
        };
    }
}