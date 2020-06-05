using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.API.Persistance.Repositories
{
    public class ToppingRepository :BaseRepository, IRepository<Topping>
    {
        public ToppingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Topping model)
        {
           await dbContext.Topping.AddAsync(model);
        }

        public async Task<Topping> FindByIdAsync(int id)
        {
            return await dbContext.Topping.FindAsync(id);
        }

        public async Task<IEnumerable<Topping>> ListAllAsync()
        {
            return await dbContext.Topping.ToListAsync();
        }

        public void Remove(Topping model)
        {
            dbContext.Topping.Remove(model);
        }

        public void Update(Topping model)
        {
            dbContext.Topping.Update(model);
        }
    }
}