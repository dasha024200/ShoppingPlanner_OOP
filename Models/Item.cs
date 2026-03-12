namespace ShoppingPlanner_OOP.Models;
public abstract class Item
{
public Guid Id { get; set; }
public string Name { get; set; }
public decimal Price { get; set; }
public Category Category { get; set; }
protected Item(string name, decimal price, Category category)
{
Id = Guid.NewGuid();
Name = name;
Price = price;
Category = category;
}
public void ChangePrice(decimal newPrice)
{
if (newPrice >= 0)
{
Price = newPrice;
}
}
public void AssignCategory(Category category)
{
Category = category;
}
public abstract string GetInfo();
}