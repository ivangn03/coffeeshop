using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;

namespace CoffeeShop.API.Services
{
    public class CoffeeService : ICRUDRepoService<Coffee, CoffeeResponse>
    {
        private readonly IRepository<Coffee> coffeeRepo;
        IUnitOfWork unitOfWork;

        public CoffeeService(IRepository<Coffee> coffeeRepo, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.coffeeRepo = coffeeRepo;
        }

        public async Task<CoffeeResponse> DeleteAsync(Coffee coffee)
        {
             var existingCoffee = await coffeeRepo.FindByIdAsync(coffee.Id);
             if(existingCoffee==null)
                return new CoffeeResponse("Coffee not found");

            try{
                coffeeRepo.Remove(existingCoffee);
                await unitOfWork.CompleteAsync();
                return new CoffeeResponse(existingCoffee);
            }catch(Exception e){
                return new CoffeeResponse(e.Message);
            }
        }

        public async Task<IEnumerable<Coffee>> ListAsync()
        {
            return await coffeeRepo.ListAllAsync();
        }

        public async Task<CoffeeResponse> SaveAsync(Coffee coffee)
        {
            try{
                await coffeeRepo.AddAsync(coffee);
                await unitOfWork.CompleteAsync();
                return new CoffeeResponse(coffee);
            }catch(Exception e){
                return new CoffeeResponse(e.Message);
            }
        }

        public async Task<CoffeeResponse> UpdateAsync(Coffee coffee)
        {
            var existingCoffee = await coffeeRepo.FindByIdAsync(coffee.Id);
            if(existingCoffee == null)
                return new CoffeeResponse("Coffee not found");
            
            existingCoffee.Name = coffee.Name;
            existingCoffee.Price = coffee.Price;
            existingCoffee.ExpirationDate = coffee.ExpirationDate;
            existingCoffee.Country = coffee.Country;
            existingCoffee.Quantity = coffee.Quantity;
            existingCoffee.Rating = coffee.Rating;
            existingCoffee.Saturation = coffee.Saturation;
            existingCoffee.Sour = coffee.Sour;
            existingCoffee.Strength = coffee.Strength;
            existingCoffee.CoffeeType = coffee.CoffeeType;
            existingCoffee.Aroma = coffee.Aroma;

            try{
                coffeeRepo.Update(existingCoffee);
                await unitOfWork.CompleteAsync();
                return new CoffeeResponse(existingCoffee);
            }catch(Exception e){
                return new CoffeeResponse(e.Message);
            }

        }
    }
}