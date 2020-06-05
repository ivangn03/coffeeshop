using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;

namespace CoffeeShop.API.Services
{
    public class ToppingService : ICRUDRepoService<Topping, ToppingResponse>
    {
        private readonly IRepository<Topping> toppingRepo;
        IUnitOfWork unitOfWork;

        public ToppingService(IRepository<Topping> toppingRepo, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.toppingRepo = toppingRepo;
        }

        public async Task<ToppingResponse> DeleteAsync(Topping topping)
        {
              var existingTopping = await toppingRepo.FindByIdAsync(topping.Id);
             if(existingTopping==null)
                return new ToppingResponse("Topping not found");

            try{
                toppingRepo.Remove(existingTopping);
                await unitOfWork.CompleteAsync();
                return new ToppingResponse(existingTopping);
            }catch(Exception e){
                return new ToppingResponse(e.Message);
            }
        }

        public async Task<IEnumerable<Topping>> ListAsync()
        {
            return await toppingRepo.ListAllAsync();
        }

        public async Task<ToppingResponse> SaveAsync(Topping topping)
        {
            try{
                await toppingRepo.AddAsync(topping);
                await unitOfWork.CompleteAsync();
                return new ToppingResponse(topping);
            }catch(Exception e){
                return new ToppingResponse(e.Message);
            }
        }

        public async Task<ToppingResponse> UpdateAsync(Topping topping)
        {
            var existingTopping = await toppingRepo.FindByIdAsync(topping.Id);
            if(existingTopping == null)
                return new ToppingResponse("Topping not found");
            existingTopping.Name = topping.Name;
            existingTopping.Price = topping.Price;
            existingTopping.ExpirationDate = topping.ExpirationDate;
            existingTopping.Quantity = topping.Quantity;
            existingTopping.Rating = topping.Rating;
            try{
                toppingRepo.Update(existingTopping);
                await unitOfWork.CompleteAsync();
                return new ToppingResponse(existingTopping);
            }catch(Exception e){
                return new ToppingResponse(e.Message);
            }

        }
    }
}