using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.API.Domain.Services
{
    public interface ICRUDRepoService<Model, Response> where Model:class where Response:class
    {
         Task<IEnumerable<Model>> ListAsync();
         Task<Response> SaveAsync(Model model);
         Task<Response> UpdateAsync(Model model);
         Task<Response> DeleteAsync(Model model);
    }
}