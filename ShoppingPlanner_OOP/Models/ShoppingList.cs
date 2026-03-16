using ShoppingPlanner_OOP.Interfaces;
using ShoppingPlanner_OOP.Services;
namespace ShoppingPlanner_OOP.Models;
public class ShoppingList
{
public Guid Id { get; set; }
public string Title { get; set; }
private readonly List<Item> _items;
private readonly List<IShoppingListObserver> _observers;
private readonly PriceCalculator _priceCalculator;
public ShoppingList(string title)
{
Id = Guid.NewGuid();
Title = title;
_items = new List<Item>();
_observers = new List<IShoppingListObserver>();
_priceCalculator = new PriceCalculator();
}
public void AddItem(Item item)
{
_items.Add(item);
NotifyObservers(_priceCalculator.CalculateTotal(_items));
}
public bool RemoveItem(Guid itemId)
{
var item = _items.FirstOrDefault(i => i.Id == itemId);
if (item == null)
{
return false;
}
_items.Remove(item);
NotifyObservers(_priceCalculator.CalculateTotal(_items));
return true;
}
public List<Item> GetItems()
{
return new List<Item>(_items);
}
public void SetItems(List<Item> items)
{
_items.Clear();
_items.AddRange(items);
NotifyObservers(_priceCalculator.CalculateTotal(_items));
}
public void AddObserver(IShoppingListObserver observer)
{
if (!_observers.Contains(observer))
{
_observers.Add(observer);
}
}
public void RemoveObserver(IShoppingListObserver observer)
{
_observers.Remove(observer);
}
public void NotifyObservers(decimal total)
{
foreach (var observer in _observers)
{
observer.Update(total);
}
}
public decimal GetTotal()
{
return _priceCalculator.CalculateTotal(_items);
}
}