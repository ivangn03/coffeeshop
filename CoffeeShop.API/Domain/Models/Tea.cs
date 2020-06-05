using System;

namespace CoffeeShop.API.Domain.Models
{
    public enum TeaType
    {
        Green,
        Red,
        Black,
        White,
        Fruit
    }
    public class Tea:Drink
    {
        
        public TeaType TeaType { get; set; }
    }
}