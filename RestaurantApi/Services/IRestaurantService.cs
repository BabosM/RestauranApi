using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.models;
using RestaurantApi.Models;
using System.Collections.Generic;

namespace RestaurantApi.Services
{
    public interface IRestaurantService
    {
        RestaurantDbContext DbContext { get; }
        IMapper Mapper { get; }

        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Put(UpdateRestaurantDto dto, int id);
    }
}