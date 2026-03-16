namespace ShoppingPlanner_OOP.Models;
public class FoodItem : Item
{
public FoodItem(string name, decimal price, Category category)
: base(name, price, category)
{
}
public override string GetInfo()
{
return $"[Food] {Name} | {Price} | {Category.Name}";
}
}