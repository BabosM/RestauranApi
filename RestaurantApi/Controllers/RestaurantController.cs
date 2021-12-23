using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Services;
using RestaurantApi.models; 

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {  
       
        private IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService){
            _restaurantService = restaurantService;
        }
     
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto){
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var createdRestaurantId = _restaurantService.Create(dto);
            return Created($"api/restaurant/{createdRestaurantId}", null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll(){
            

           var restaurantsDto = _restaurantService.GetAll();
            return Ok(restaurantsDto);
        }
        //tesGit
        [HttpGet("{id}")]
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