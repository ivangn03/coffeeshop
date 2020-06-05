using System.Threading.Tasks;

namespace CoffeeShop.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}