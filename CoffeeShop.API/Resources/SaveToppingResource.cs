using System;
using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Resources
{
    public class SaveToppingResource
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public double Rating { get; set; }
    }
}