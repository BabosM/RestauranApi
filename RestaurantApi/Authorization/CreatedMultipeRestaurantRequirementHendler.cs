using Microsoft.AspNetCore.Authorization;
using RestaurantApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantApi.Authorization
{
    public class CreatedMultipeRestaurantRequirementHendler : AuthorizationHandler<CreatedMultipeRestaurantRequirement>
    {
        private readonly RestaurantDbContext _context;

        public CreatedMultipeRestaurantRequirementHendler(RestaurantDbContext context)
        {
            _context = context;
        }
        

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipeRestaurantRequirement requirement)
        {
           var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdRestaurantsCount = _context.Restaurants.Count(x => x.CreatedById == userId);
            if(createdRestaurantsCount >= requirement.minimumCreatedRestaurants)
            {          
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
