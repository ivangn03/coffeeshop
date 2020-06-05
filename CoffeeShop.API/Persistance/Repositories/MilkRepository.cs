using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.API.Persistance.Repositories
{
    public class MilkRepository :BaseRepository, IRepository<Milk>
    {
        public MilkRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Milk model)
        {
           await dbContext.Milk.AddAsync(model);
        }

        public async Task<Milk> FindByIdAsync(int id)
        {
            return await dbContext.Milk.FindAsync(id);
        }

        public async Task<IEnumerable<Milk>> ListAllAsync()
        {
            return await dbContext.Milk.ToListAsync();
        }

        public void Remove(Milk model)
        {
            dbContext.Milk.Remove(model);
        }

        public void Update(Milk model)
        {
            dbContext.Milk.Update(model);
        }
    }
}