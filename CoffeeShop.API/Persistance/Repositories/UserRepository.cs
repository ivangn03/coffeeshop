using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.API.Persistance.Repositories
{
    public class UserRepository:BaseRepository, IRepository<User>
    {
     public UserRepository(AppDbContext dbContext):base(dbContext)
     {
         
     }

        public async Task AddAsync(User user)
        {
               await dbContext.Users.AddAsync(user);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> ListAllAsync()
        {
            return await dbContext.Users.Include(x=>x.UserRoles).ThenInclude(x=>x.Role).ToListAsync();
            //through navigation property UserRoles in Users going to navigation property in UserRoles directly to a selecting in Collection of User-Role Pairs by nav prop role
        }

        public void Update(User user)
        {
              dbContext.Users.Update(user);
        }
        public void Remove(User user){
            dbContext.Users.Remove(user);
        }
    }
}