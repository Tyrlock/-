namespace CoreLibarary.Models
{
    using System;

    public enum Category
    {
        Drinks = 1,
        Alcohol = 2,
        HotDishes = 3,
        Salads = 4,
        Juices = 5
    }

    public class Dish
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public Category CategoryId { get; set; }
        public byte[] image { get; set;}
        public int UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
