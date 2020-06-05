using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.API.Persistance.Repositories
{
    public class CoffeeRepository :BaseRepository, IRepository<Coffee>
    {
        public CoffeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Coffee model)
        {
           await dbContext.Coffee.AddAsync(model);
        }

        public async Task<Coffee> FindByIdAsync(int id)
        {
            return await dbContext.Coffee.FindAsync(id);
        }

        public async Task<IEnumerable<Coffee>> ListAllAsync()
        {
            return await dbContext.Coffee.ToListAsync();
        }

        public void Remove(Coffee model)
        {
            dbContext.Coffee.Remove(model);
        }

        public void Update(Coffee model)
        {
            dbContext.Coffee.Update(model);
        }
    }
}