using System;

namespace CoffeeShop.API.Domain.Models
{
    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public double Rating { get; set; }
    }
}