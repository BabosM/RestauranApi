using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
    }
    public class DishService:IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == restaurantId);

            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            var dishEntiry = _mapper.Map<Dish>(dto);
            dishEntiry.RestaurantId = restaurantId;
            _context.Dishes.Add(dishEntiry);
            _context.SaveChanges();
            return dishEntiry.Id;
            /// ogarniecie bazy danych poniewaz id dodal sie nie do tej kolumny 
        }
    }
}
