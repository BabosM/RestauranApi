using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.models;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public RestaurantDbContext DbContext { get; }
        public IMapper Mapper { get; }

        public RestaurantDto GetById(int id)
        {

            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not Found");
            }
            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }
        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} has been invoke"); 
            var restaurant = _dbContext.
                  Restaurants
                  .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) {
                _logger.LogError($"Restaurant with id: {id} not exists");
                throw new NotFoundException("Restaurant not Found");
            }
            else 
            {
                _dbContext.Restaurants.Remove(restaurant);
                _dbContext.SaveChanges();
                _logger.LogWarning($"Restaurant with id: {id} has been deleted");             
            }    
        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                 .Restaurants
                 .Include(r => r.Address)
                 .Include(r => r.Dishes)
                 .ToList();
            var result = _mapper.Map<List<RestaurantDto>>(restaurants);
            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Put(UpdateRestaurantDto dto, int id)
        {
            var restaurantForUpdate = _dbContext
                 .Restaurants
                 .FirstOrDefault(r => r.Id == id);
            if (restaurantForUpdate == null) {
                throw new NotFoundException("Restaurant not Found");             
            }
            restaurantForUpdate.Name = dto.Name;
            restaurantForUpdate.Description = dto.Description;
            restaurantForUpdate.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
            


        }
    }
}
