using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.models;
using RestaurantApi.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestaurantApi.Services
{
    public interface IRestaurantService
    {
        RestaurantDbContext DbContext { get; }
        IMapper Mapper { get; }

        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        PagedResult<RestaurantDto> GetAll( RestaurantQuery query);

        RestaurantDto GetById(int id);
        void Put(UpdateRestaurantDto dto, int id);
    }
}