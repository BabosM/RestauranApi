using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Services;
using RestaurantApi.models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
 
    public class RestaurantController : ControllerBase
    {  
       
        private IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService){
            _restaurantService = restaurantService;
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto){
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var userId = int.Parse( User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdRestaurantId = _restaurantService.Create(dto);
            return Created($"api/restaurant/{createdRestaurantId}", null);
        }
        [HttpGet]

     //  [Authorize(Policy = "Atleast2restaurants")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query){


  

           var restaurantsDto = _restaurantService.GetAll(query);
            return Ok(restaurantsDto);
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "HasNationality")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id){

            var restaurantDto = _restaurantService.GetById(id);   
            return Ok(restaurantDto);

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)  {

            _restaurantService.Delete(id);        
                      
                return NoContent();          
   
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _restaurantService.Put(dto, id);      
            return Ok();
                
        }
    }
}