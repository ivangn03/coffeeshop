using CoffeeShop.API.Domain.Models;

namespace CoffeeShop.API.Domain.Services.Communication
{
    public class AuthResponse:BaseResponse
    {
        public User User { get; set; }
        public AuthResponse(bool success, string message, User user) : base(success, message)
        {
            User = user;
        }
        public AuthResponse(User user):this(true, string.Empty, user){}
        public AuthResponse(string message):this(false, message, null){}
    }
}