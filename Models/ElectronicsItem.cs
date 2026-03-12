namespace ShoppingPlanner_OOP.Models;

public class ElectronicsItem : Item
{
public ElectronicsItem(string name, decimal price, Category category)
: base(name, price, category)
{
}
public override string GetInfo()
{
return $"[Electronics] {Name} | {Price} | {Category.Name}";
}
}