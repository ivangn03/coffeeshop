using System;
using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Resources
{
    public class SaveCoffeeResource
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public double Rating { get; set; }
             public string Country { get; set; }
        public CoffeeType CoffeeType { get; set; }
        public double? Sour { get; set; }
        public double? Strength { get; set; }
        public double? Saturation { get; set; }
        public double? Aroma { get; set; }
    }
}