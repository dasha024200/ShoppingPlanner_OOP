using ShoppingPlanner_OOP.Models;
namespace ShoppingPlanner_OOP.Interfaces;
public interface ISortStrategy
{
List<Item> Sort(List<Item> items);
}