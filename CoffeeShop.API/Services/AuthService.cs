using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Repositories;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;
using CoffeeShop.API.Helpers;
using Microsoft.Extensions.Options;
using CoffeeShop.API.Extensions;
namespace CoffeeShop.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings appSettings;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<User> userRepository;

        public AuthService(IOptions<AppSettings> appSettingsOptions, IRepository<User> userRepository,IUnitOfWork unitOfWork)
        {
            this.appSettings = appSettingsOptions.Value;
            this.userRepository =userRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<AuthResponse> AuthenticateAsync(string login, string password)///It's ok to pass email in login field for Authentication
        {
           User userAuthed =( await  userRepository.ListAllAsync()).SingleOrDefault(
               usr=>{
                   return (usr.Login ==login || usr.Email == login)&& usr.Password == password;
               }
           );
           if(userAuthed!=null)
           {
                userAuthed.GenerateToken(appSettings.Secret, appSettings.ExpiresMinutes);
                return new AuthResponse(userAuthed);
           }
            var userExisted =(await  userRepository.ListAllAsync()).SingleOrDefault(
               usr=> usr.Login == login|| usr.Email ==login
           );
           if(userExisted!= null)
                return new AuthResponse("The password is incorect.");
            else
                return new AuthResponse("User not found");
        }


    }
}