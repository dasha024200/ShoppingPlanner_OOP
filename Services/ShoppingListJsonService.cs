using System.Text.Json;
using ShoppingPlanner_OOP.Models;

namespace ShoppingPlanner_OOP.Services
{
    public class ShoppingListJsonService
    {
        public void SaveToJson(ShoppingList shoppingList, string filePath)
        {
            ShoppingListDto dto = new ShoppingListDto
            {
                Id = shoppingList.Id,
                Title = shoppingList.Title,
                Items = shoppingList.GetItems().Select(item => new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Category = new CategoryDto
                    {
                        Id = item.Category.Id,
                        Name = item.Category.Name
                    }
                }).ToList()
            };

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(dto, options);
            File.WriteAllText(filePath, json);
        }

        public ShoppingList LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл JSON не знайдено.", filePath);
            }

            string json = File.ReadAllText(filePath);

            ShoppingListDto? dto = JsonSerializer.Deserialize<ShoppingListDto>(json);

            if (dto == null)
            {
                throw new Exception("Не вдалося завантажити список покупок із JSON.");
            }

            ShoppingList shoppingList = new ShoppingList(dto.Title)
            {
                Id = dto.Id
            };

            foreach (ItemDto itemDto in dto.Items)
            {
                Category category = new Category(itemDto.Category.Name)
                {
                    Id = itemDto.Category.Id
                };

                Item item = new Item(itemDto.Name, itemDto.Price, category)
                {
                    Id = itemDto.Id
                };

                shoppingList.AddItem(item);
            }

            return shoppingList;
        }
    }

    public class ShoppingListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<ItemDto> Items { get; set; } = new();
    }

    public class ItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public CategoryDto Category { get; set; } = new();
    }

    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}