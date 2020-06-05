using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Domain.Services.Communication
{
    public class MilkResponse:BaseResponse
    {
        public Milk Milk { get; set; }
        public MilkResponse(bool success, string message, Milk milk) : base(success, message)
        {
            Milk = milk;
        }
        public MilkResponse(Milk milk):this(true, string.Empty, milk){}
        public MilkResponse(string message):this(false, message, null){}
    }
}