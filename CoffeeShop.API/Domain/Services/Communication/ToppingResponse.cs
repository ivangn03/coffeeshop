using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Domain.Services.Communication
{
    public class ToppingResponse:BaseResponse
    {
        public Topping Topping { get; set; }
        public ToppingResponse(bool success, string message, Topping topping) : base(success, message)
        {
            Topping = topping;
        }
        public ToppingResponse(Topping topping):this(true, string.Empty, topping){}
        public ToppingResponse(string message):this(false, message, null){}
    }
}