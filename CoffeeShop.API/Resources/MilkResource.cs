using System;
using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Resources
{
    public class MilkResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public MilkType MilkType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Fattiness { get; set; }
        public double Rating { get; set; }
    }
}