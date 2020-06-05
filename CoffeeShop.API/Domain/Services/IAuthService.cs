using System.Threading.Tasks;
using CoffeeShop.API.Domain.Services.Communication;

namespace CoffeeShop.API.Domain.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(string login, string password);
    }
}