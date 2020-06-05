using System.Linq;
using AutoMapper;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Resources;

namespace CoffeeShop.API.Mapping
{
    public class ModelToResourceProfile:Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>().ForMember(dest => dest.Role, src=>src.MapFrom(x => x.UserRoles.Select(y=>y.Role.Name)));
            CreateMap<Coffee, CoffeeResource>();
            CreateMap<Milk, MilkResource>();
            CreateMap<Topping, ToppingResource>();
            CreateMap<Tea, TeaResource>();
        }
    }
}