using System;
using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Resources
{
    public class TeaResource
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public TeaType TeaType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public string Country { get; set; }
        public double? Rating { get; set; }
    }
}