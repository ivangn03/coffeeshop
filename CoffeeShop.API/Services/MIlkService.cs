using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;

namespace CoffeeShop.API.Services
{
    public class MilkService : ICRUDRepoService<Milk, MilkResponse>
    {
        private readonly IRepository<Milk> milkRepo;
        IUnitOfWork unitOfWork;

        public MilkService(IRepository<Milk> milkRepo, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.milkRepo = milkRepo;
        }

        public async Task<MilkResponse> DeleteAsync(Milk milk)
        {
             var existingMilk = await milkRepo.FindByIdAsync(milk.Id);
             if(existingMilk==null)
                return new MilkResponse("Milk not found");

            try{
                milkRepo.Remove(existingMilk);
                await unitOfWork.CompleteAsync();
                return new MilkResponse(existingMilk);
            }catch(Exception e){
                return new MilkResponse(e.Message);
            }
        }

        public async Task<IEnumerable<Milk>> ListAsync()
        {
            return await milkRepo.ListAllAsync();
        }

        public async Task<MilkResponse> SaveAsync(Milk milk)
        {
            try{
                await milkRepo.AddAsync(milk);
                await unitOfWork.CompleteAsync();
                return new MilkResponse(milk);
            }catch(Exception e){
                return new MilkResponse(e.Message);
            }
        }

        public async Task<MilkResponse> UpdateAsync(Milk milk)
        {
            var existingMilk = await milkRepo.FindByIdAsync(milk.Id);
            if(existingMilk == null)
                return new MilkResponse("Milk not found");
            
            existingMilk.Name = milk.Name;
            existingMilk.Price = milk.Price;
            existingMilk.Quantity = milk.Quantity;
            existingMilk.Rating = milk.Rating;
            existingMilk.Fattiness = milk.Fattiness;
            existingMilk.ExpirationDate = milk.ExpirationDate;
            try{
                milkRepo.Update(existingMilk);
                await unitOfWork.CompleteAsync();
                return new MilkResponse(existingMilk);
            }catch(Exception e){
                return new MilkResponse(e.Message);
            }

        }
    }
}