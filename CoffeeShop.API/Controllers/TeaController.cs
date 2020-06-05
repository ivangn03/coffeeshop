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
    public class TeaController:Controller
    {
        private readonly ICRUDRepoService<Tea, TeaResponse> teaService;
        private readonly IMapper mapper;
        public TeaController(ICRUDRepoService<Tea, TeaResponse> teaService, IMapper mapper)
        {
            this.teaService = teaService;
            this.mapper = mapper;
        }
         [HttpGet]
        public async Task<IEnumerable<TeaResource>> GetAllAsync()
        {
            var tea = await teaService.ListAsync();
            var resource = mapper.Map<IEnumerable<Tea>, IEnumerable<TeaResource>>(tea);
            
            return resource;
        }
[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTeaResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tea = mapper.Map<SaveTeaResource, Tea>(resource);
            var result = await teaService.SaveAsync(tea);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message = result.Message, Success = result.Success});

            var teaResource = mapper.Map<Tea, TeaResource>(result.Tea);
            return Ok(new ResponseData{Data = teaResource, Message = "", Success = true});
        }
[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveTeaResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tea = mapper.Map<SaveTeaResource, Tea>(resource);
            tea.Id = id;
            var result = await teaService.UpdateAsync(tea);

            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var teaResource = mapper.Map<Tea, TeaResource>(result.Tea);
            return Ok(new ResponseData{Data = teaResource, Message = "", Success = true});
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteTeaResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var tea = mapper.Map<DeleteTeaResource, Tea>(resource);
            var result = await teaService.DeleteAsync(tea);
            if (!result.Success)
                return Ok(new ResponseData{Data = null, Message =result.Message, Success = result.Success});

            var teaResource = mapper.Map<Tea, TeaResource>(result.Tea);
            return Ok(new ResponseData{Data = teaResource, Message = "", Success = true});
        }
        
    }
}