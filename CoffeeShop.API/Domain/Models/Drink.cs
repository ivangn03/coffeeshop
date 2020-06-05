using System;

namespace CoffeeShop.API.Domain.Models
{
    public abstract class Drink{
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public string Country { get; set; }
        public double? Rating { get; set; }
    }
}