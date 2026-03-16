using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Tests;

public class CategoryTests
{
    [Fact]
    public void Rename_WithValidValue_ChangesName()
    {
        var category = new Category("Food");

        category.Rename("Household");

        Assert.Equal("Household", category.Name);
    }

    [Fact]
    public void Rename_WithWhitespace_DoesNotChangeName()
    {
        var category = new Category("Food");

        category.Rename("   ");

        Assert.Equal("Food", category.Name);
    }

    [Fact]
    public void IsMatch_IgnoresCase_ReturnsTrue()
    {
        var category = new Category("Food");

        var result = category.IsMatch("food");

        Assert.True(result);
    }
}