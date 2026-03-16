namespace ShoppingPlanner_OOP.Models;
public class HouseholdItem : Item
{
public HouseholdItem(string name, decimal price, Category category)
: base(name, price, category)
{
}
public override string GetInfo()
{
return $"[Household] {Name} | {Price} | {Category.Name}";
}
}