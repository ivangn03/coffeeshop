using System;

namespace CoffeeShop.API.Domain.Models
{
    public enum CoffeeType
    {
        Arabica,
        Robusta
    }
    public class Coffee:Drink
    {
        public CoffeeType CoffeeType { get; set; }
        public double? Sour { get; set; }
        public double? Strength { get; set; }
        public double? Saturation { get; set; }
        public double? Aroma { get; set; }
    }
}