using  RestaurantApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace  RestaurantApi
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext){
            _dbContext = dbContext;
	}
        
        public void Seed(){
            if(_dbContext.Database.CanConnect()){

                if(!_dbContext.Restaurants.Any()){
                    
                   var restaurants = GetRestaurants(); 
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                    
                }
            }

        }
        public IEnumerable<Restaurant> GetRestaurants(){
            var restaurants = new List<Restaurant>(){
                new Restaurant(){
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Najlepszy kurczak w mieście",
                    HasDelivery = true,
                    Dishes = new List<Dish>(){
                      new Dish(){
                          Name = "Kurczak w panierce",
                          Price = 10.30M,
                      },
                      new Dish(){
                          Name = "Indyk na ostro",
                          Price = 8.90M
                      },                    
                    },
                    Address = new Address(){
                        Street = "Henryków 35",
                        City = "Lubochnia",
                        PostalCode = "97-217"
                    }
                }
            
            };
            return restaurants;
        }
    }
}