using System.Text.Json;
using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Services;
public class ShoppingListJsonService
{
private readonly DefaultItemFactory _factory;
public ShoppingListJsonService(DefaultItemFactory factory)
{
_factory = factory;
}
public void Save(string filePath, ShoppingList shoppingList)
{
var data = new ShoppingListDto
{
Id = shoppingList.Id,
Title = shoppingList.Title,
Items = shoppingList.GetItems().Select(item => new ItemDto
{
Id = item.Id,
Name = item.Name,
Price = item.Price,
CategoryId = item.Category.Id,
CategoryName = item.Category.Name,
ItemType = item.GetType().Name
}).ToList()
};
var options = new JsonSerializerOptions
{
WriteIndented = true
};
string json = JsonSerializer.Serialize(data, options);
File.WriteAllText(filePath, json);
}
public ShoppingList? Load(string filePath)
{
if (!File.Exists(filePath))
{
return null;
}
string json = File.ReadAllText(filePath);
var data = JsonSerializer.Deserialize<ShoppingListDto>(json);
if (data == null)
{
return null;
}
var shoppingList = new ShoppingList(data.Title)
{
Id = data.Id
};
var items = new List<Item>();
foreach (var itemDto in data.Items)
{
var category = new Category(itemDto.CategoryName)
{
Id = itemDto.CategoryId
};
Item item = itemDto.ItemType switch
{
nameof(FoodItem) => new FoodItem(itemDto.Name,
itemDto.Price, category),
nameof(HouseholdItem) => new HouseholdItem(itemDto.Name,
itemDto.Price, category),
nameof(ElectronicsItem) => new ElectronicsItem(itemDto.Name,
itemDto.Price, category),
_ => _factory.CreateItem(itemDto.Name, itemDto.Price,
category)
};
item.Id = itemDto.Id;
items.Add(item);
}
shoppingList.SetItems(items);
return shoppingList;
}
private class ShoppingListDto
{
public Guid Id { get; set; }
public string Title { get; set; } = string.Empty;
public List<ItemDto> Items { get; set; } = new();
}
private class ItemDto
{
public Guid Id { get; set; }
public string Name { get; set; } = string.Empty;
public decimal Price { get; set; }
public Guid CategoryId { get; set; }
public string CategoryName { get; set; } = string.Empty;
public string ItemType { get; set; } = string.Empty;
}
}