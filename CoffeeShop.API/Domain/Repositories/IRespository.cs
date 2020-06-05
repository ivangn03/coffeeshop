using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.API.Domain.Repositories
{
   public interface IRepository<T> where T:class
   {
       Task<IEnumerable<T>> ListAllAsync();
       Task AddAsync(T model);
       void Update(T model);
       Task<T> FindByIdAsync(int id);
       void Remove(T model);       
   }
}