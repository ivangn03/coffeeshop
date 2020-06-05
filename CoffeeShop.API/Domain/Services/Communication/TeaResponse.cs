using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Domain.Services.Communication
{
    public class TeaResponse:BaseResponse
    {
        public Tea Tea { get; set; }
        public TeaResponse(bool success, string message, Tea tea) : base(success, message)
        {
            Tea = tea;
        }
        public TeaResponse(Tea tea):this(true, string.Empty, tea){}
        public TeaResponse(string message):this(false, message, null){}
    }
}