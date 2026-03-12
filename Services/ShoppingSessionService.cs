using System.Text.Json;
using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Services;

public class ShoppingSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string ShoppingListKey = "SHOPPING_LIST";
    private const string BudgetKey = "BUDGET_LIMIT";

    public ShoppingSessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public ShoppingList GetShoppingList()
    {
        var json = Session.GetString(ShoppingListKey);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new ShoppingList("My Shopping List");
        }

        var data = JsonSerializer.Deserialize<ShoppingListSessionDto>(json);

        if (data == null)
        {
            return new ShoppingList("My Shopping List");
        }

        var list = new ShoppingList(data.Title)
        {
            Id = data.Id
        };

        var items = new List<Item>();

        foreach (var dto in data.Items)
        {
            var category = new Category(dto.CategoryName)
            {
                Id = dto.CategoryId
            };

            Item item = dto.ItemType switch
            {
                nameof(FoodItem) => new FoodItem(dto.Name, dto.Price, category),
                nameof(HouseholdItem) => new HouseholdItem(dto.Name, dto.Price, category),
                nameof(ElectronicsItem) => new ElectronicsItem(dto.Name, dto.Price, category),
                _ => new FoodItem(dto.Name, dto.Price, category)
            };

            item.Id = dto.Id;

            items.Add(item);
        }

        list.SetItems(items);

        return list;
    }

    public void SaveShoppingList(ShoppingList shoppingList)
    {
        var dto = new ShoppingListSessionDto
        {
            Id = shoppingList.Id,
            Title = shoppingList.Title,
            Items = shoppingList.GetItems().Select(item => new ItemSessionDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CategoryId = item.Category.Id,
                CategoryName = item.Category.Name,
                ItemType = item.GetType().Name
            }).ToList()
        };

        var json = JsonSerializer.Serialize(dto);

        Session.SetString(ShoppingListKey, json);
    }

    public void SetBudgetLimit(decimal limit)
    {
        Session.SetString(
            BudgetKey,
            limit.ToString(System.Globalization.CultureInfo.InvariantCulture)
        );
    }

    public decimal GetBudgetLimit()
    {
        var value = Session.GetString(BudgetKey);

        if (decimal.TryParse(
            value,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out decimal limit))
        {
            return limit;
        }

        return 0;
    }

    private class ShoppingListSessionDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public List<ItemSessionDto> Items { get; set; } = new();
    }

    private class ItemSessionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string ItemType { get; set; } = string.Empty;
    }
}