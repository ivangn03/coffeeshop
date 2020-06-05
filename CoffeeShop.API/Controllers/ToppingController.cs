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
    public class ToppingController:Controller
    {
        private readonly ICRUDRepoService<Topping, ToppingResponse> toppingService;
        private readonly IMapper mapper;
        public ToppingController(ICRUDRepoService<Topping, ToppingResponse> toppingService, IMapper mapper)
        {
            this.toppingService = toppingService;
            this.mapper = mapper;
        }
         [HttpGet]
        public async Task<IEnumerable<ToppingResource>> GetAllAsync()
        {
            var topping = await toppingService.ListAsync();
            var resource = mapper.Map<IEnumerable<Topping>, IEnumerable<ToppingResource>>(topping);
            
            return resource;
        }
[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveToppingResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveToppingResource, Topping>(resource);
            var result = await toppingService.SaveAsync(user);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message = result.Message, Success = result.Success});

            var toppingResource = mapper.Map<Topping, ToppingResource>(result.Topping);
            return Ok(new ResponseData{Data = toppingResource, Message = "", Success = true});
        }
[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveToppingResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveToppingResource, Topping>(resource);
            user.Id = id;
            var result = await toppingService.UpdateAsync(user);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var toppingResource = mapper.Map<Topping, ToppingResource>(result.Topping);
            return Ok(new ResponseData{Data = toppingResource, Message = "", Success = true});
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteToppingResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var user = mapper.Map<DeleteToppingResource, Topping>(resource);
            var result = await toppingService.DeleteAsync(user);
            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var toppingResource = mapper.Map<Topping, ToppingResource>(result.Topping);
            return Ok(new ResponseData{Data = toppingResource, Message = "", Success = true});
        }
        
    }
}