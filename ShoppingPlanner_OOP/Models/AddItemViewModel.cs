using System.ComponentModel.DataAnnotations;
namespace ShoppingPlanner_OOP.Models;
public class AddItemViewModel
{
[Required(ErrorMessage = "Введи назву товару")]
public string Name { get; set; } = string.Empty;
[Range(0.01, 1000000, ErrorMessage = "Ціна має бути більшою за 0")]
public decimal Price { get; set; }
[Required(ErrorMessage = "Введи категорію")]
public string CategoryName { get; set; } = "Food";
}