using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;

namespace CoffeeShop.API.Services
{
    public class TeaService : ICRUDRepoService<Tea, TeaResponse>
    {
        private readonly IRepository<Tea> teaRepo;
        IUnitOfWork unitOfWork;

        public TeaService(IRepository<Tea> teaRepo, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.teaRepo = teaRepo;
        }

        public async Task<TeaResponse> DeleteAsync(Tea tea)
        {
              var existingTea = await teaRepo.FindByIdAsync(tea.Id);
             if(existingTea==null)
                return new TeaResponse("Tea not found");

            try{
                teaRepo.Remove(existingTea);
                await unitOfWork.CompleteAsync();
                return new TeaResponse(existingTea);
            }catch(Exception e){
                return new TeaResponse(e.Message);
            }
        }

        public async Task<IEnumerable<Tea>> ListAsync()
        {
            return await teaRepo.ListAllAsync();
        }

        public async Task<TeaResponse> SaveAsync(Tea tea)
        {
            try{
                await teaRepo.AddAsync(tea);
                await unitOfWork.CompleteAsync();
                return new TeaResponse(tea);
            }catch(Exception e){
                return new TeaResponse(e.Message);
            }
        }

        public async Task<TeaResponse> UpdateAsync(Tea tea)
        {
            var existingTea = await teaRepo.FindByIdAsync(tea.Id);
            if(existingTea == null)
                return new TeaResponse("Tea not found");
            existingTea.Name = tea.Name;
            existingTea.Price = tea.Price;
            existingTea.ExpirationDate = tea.ExpirationDate;
            existingTea.Country = tea.Country;
            existingTea.Quantity = tea.Quantity;
            existingTea.Rating = tea.Rating;
            existingTea.TeaType =tea.TeaType;
            try{
                teaRepo.Update(existingTea);
                await unitOfWork.CompleteAsync();
                return new TeaResponse(existingTea);
            }catch(Exception e){
                return new TeaResponse(e.Message);
            }

        }
    }
}