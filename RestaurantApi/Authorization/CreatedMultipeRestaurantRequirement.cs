using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Authorization
{
    public class CreatedMultipeRestaurantRequirement : IAuthorizationRequirement
    {
        public int minimumCreatedRestaurants { get; }
       
        public CreatedMultipeRestaurantRequirement(int minimumCreatedRestaurants)
        {
            this.minimumCreatedRestaurants = minimumCreatedRestaurants;
        }
    }
}
