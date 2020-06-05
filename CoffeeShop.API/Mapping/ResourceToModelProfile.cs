using AutoMapper;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Resources;

namespace CoffeeShop.API.Mapping
{
    public class ResourceToModelProfile:Profile
    {
         public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveCoffeeResource, Coffee>();
            CreateMap<SaveTeaResource, Tea>();
            CreateMap<SaveMilkResource, Milk>();
            CreateMap<SaveToppingResource, Topping>();
            CreateMap<DeleteUserResource, User>();
            CreateMap<DeleteCoffeeResource, Coffee>();
            CreateMap<DeleteTeaResource, Tea>();
            CreateMap<DeleteMilkResource, Milk>();
            CreateMap<DeleteToppingResource, Topping>();
        }
    }
}