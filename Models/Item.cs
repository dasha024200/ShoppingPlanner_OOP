namespace ShoppingPlanner_OOP.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }

        public Item(string name, decimal price, Category category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Category = category;
        }
    }
}