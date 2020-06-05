using System.Threading.Tasks;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Persistance.Context;

namespace CoffeeShop.API.Persistance.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private  readonly AppDbContext dbContext;
        public UnitOfWork(AppDbContext context)
        {
            dbContext = context;
        }
        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}