using CoffeeShop.API.Persistance.Context;

namespace CoffeeShop.API.Persistance.Repositories
{
    public abstract class BaseRepository
    {
         protected readonly AppDbContext dbContext;
         public BaseRepository(AppDbContext context)
         {
             dbContext = context;
         }
    }
}