using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;

namespace CoffeeShop.API.Services

{
    public class UserService: ICRUDRepoService<User, UserResponse>
    {
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork)
        {
            userRepository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteAsync(User user)
        {
            var existingUser = await userRepository.FindByIdAsync(user.Id);
            if(existingUser==null)
                return new UserResponse("User not found");

            try
            {
                userRepository.Remove(existingUser);
                await unitOfWork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
               return new UserResponse($"User delete error:${ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAllAsync();
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Saving failed: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(User user)
        {
            var existingUser = await userRepository.FindByIdAsync(user.Id);
            if(existingUser ==null)
                return new UserResponse("User not found");
            
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Login = user.Login;
            
            try
            {
                userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Update error: {ex.Message}");
            }
            
        }

    }
}