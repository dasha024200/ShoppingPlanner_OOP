using System.Text;
using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Services;

public class CsvExportService
{
    public byte[] Export(List<Item> items)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Name,Price,Category,Type");

        foreach (var item in items)
        {
            sb.AppendLine($"{item.Id},{Escape(item.Name)},{item.Price},{Escape(item.Category.Name)},{item.GetType().Name}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private string Escape(string value)
    {
        if (value.Contains(',') || value.Contains('\"') || value.Contains('\n'))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }

        return value;
    }
}