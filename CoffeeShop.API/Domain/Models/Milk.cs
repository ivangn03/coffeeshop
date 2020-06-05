using System;

namespace CoffeeShop.API.Domain.Models
{
    public enum MilkType{
        Cow,
        Almond,
        Buckwheat
    }
    public class Milk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public MilkType MilkType { get; set; }
        public double Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Fattiness { get; set; }
        public double Rating { get; set; }
    }
}