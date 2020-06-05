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
    public class MilkController:Controller
    {
        private readonly ICRUDRepoService<Milk, MilkResponse> milkService;
        private readonly IMapper mapper;
        public MilkController(ICRUDRepoService<Milk, MilkResponse> milkService, IMapper mapper)
        {
            this.milkService = milkService;
            this.mapper = mapper;
        }
         [HttpGet]
        public async Task<IEnumerable<MilkResource>> GetAllAsync()
        {
            var milk = await milkService.ListAsync();
            var resource = mapper.Map<IEnumerable<Milk>, IEnumerable<MilkResource>>(milk);
            
            return resource;
        }
[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveMilkResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var milk = mapper.Map<SaveMilkResource, Milk>(resource);
            var result = await milkService.SaveAsync(milk);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message = result.Message, Success = result.Success});

            var milkResource = mapper.Map<Milk, MilkResource>(result.Milk);
            return Ok(new ResponseData{Data = milkResource, Message = "", Success = true});
        }
[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveMilkResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var milk = mapper.Map<SaveMilkResource, Milk>(resource);
            milk.Id = id;
            var result = await milkService.UpdateAsync(milk);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var milkResource = mapper.Map<Milk, MilkResource>(result.Milk);
            return Ok(new ResponseData{Data = milkResource, Message = "", Success = true});
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteMilkResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var milk = mapper.Map<DeleteMilkResource, Milk>(resource);
            var result = await milkService.DeleteAsync(milk);
            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var milkResource = mapper.Map<Milk, MilkResource>(result.Milk);
            return Ok(new ResponseData{Data = milkResource, Message = "", Success = true});
        }
        
    }
}