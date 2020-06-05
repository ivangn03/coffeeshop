using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.API.Domain.Models;
using CoffeeShop.API.Domain.Services;
using CoffeeShop.API.Domain.Services.Communication;
using CoffeeShop.API.Resources;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CoffeeShop.API.Controllers
{
   [Route("/api/[controller]")]
    public class CoffeeController:Controller
    {
        private readonly ICRUDRepoService<Coffee, CoffeeResponse> coffeeService;
        private readonly IMapper mapper;
        public CoffeeController(ICRUDRepoService<Coffee, CoffeeResponse> coffeeService, IMapper mapper)
        {
            this.coffeeService = coffeeService;
            this.mapper = mapper;
        }
         [HttpGet]
        public async Task<IEnumerable<CoffeeResource>> GetAllAsync()
        {
            var coffee = await coffeeService.ListAsync();
            var resource = mapper.Map<IEnumerable<Coffee>, IEnumerable<CoffeeResource>>(coffee);
            
            return resource;
        }
[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCoffeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var coffee = mapper.Map<SaveCoffeeResource, Coffee>(resource);
            var result = await coffeeService.SaveAsync(coffee);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message = result.Message, Success = result.Success});

            var coffeeResource = mapper.Map<Coffee, CoffeeResource>(result.Coffee);
            return Ok(new ResponseData{Data = coffeeResource, Message = "", Success = true});
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCoffeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var coffee = mapper.Map<SaveCoffeeResource, Coffee>(resource);
            coffee.Id = id;
            var result = await coffeeService.UpdateAsync(coffee);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var coffeeResource = mapper.Map<Coffee, CoffeeResource>(result.Coffee);
            return Ok(new ResponseData{Data = coffeeResource, Message = "", Success = true});
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteCoffeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var coffee = mapper.Map<DeleteCoffeeResource, Coffee>(resource);
            var result = await coffeeService.DeleteAsync(coffee);
            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var coffeeResource = mapper.Map<Coffee, CoffeeResource>(result.Coffee);
            return Ok(new ResponseData{Data = coffeeResource, Message = "", Success = true});
        }
        
    }
}