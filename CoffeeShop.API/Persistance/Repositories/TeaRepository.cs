using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.API.Persistance.Repositories
{
    public class TeaRepository :BaseRepository, IRepository<Tea>
    {
        public TeaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Tea model)
        {
           await dbContext.Tea.AddAsync(model);
        }

        public async Task<Tea> FindByIdAsync(int id)
        {
            return await dbContext.Tea.FindAsync(id);
        }

        public async Task<IEnumerable<Tea>> ListAllAsync()
        {
            return await dbContext.Tea.ToListAsync();
        }

        public void Remove(Tea model)
        {
            dbContext.Tea.Remove(model);
        }

        public void Update(Tea model)
        {
            dbContext.Tea.Update(model);
        }
    }
}