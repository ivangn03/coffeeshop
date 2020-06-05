using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Domain.Services.Communication
{
    public class CoffeeResponse:BaseResponse
    {
        public Coffee Coffee { get; set; }
        public CoffeeResponse(bool success, string message, Coffee coffee) : base(success, message)
        {
            Coffee = coffee;
        }
        public CoffeeResponse(Coffee coffee):this(true, string.Empty, coffee){}
        public CoffeeResponse(string message):this(false, message, null){}
    }
}