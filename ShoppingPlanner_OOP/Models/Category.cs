namespace ShoppingPlanner_OOP.Models;
public class Category
{
public Guid Id { get; set; }
public string Name { get; set; }
public Category()
{
Id = Guid.NewGuid();
Name = string.Empty;
}
public Category(string name)
{
Id = Guid.NewGuid();
Name = name;
}
public void Rename(string newName)
{
if (!string.IsNullOrWhiteSpace(newName))
{
Name = newName.Trim();
}
}
public bool IsMatch(string categoryName)
{
return Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase);
}
}