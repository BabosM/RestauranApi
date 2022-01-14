using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantApi.Authorization
{
    public class MinimumAgeRequirementHendler : AuthorizationHandler<MinimumAgeRequirement>
    {
        public readonly ILogger<MinimumAgeRequirementHendler> _logger;
        public MinimumAgeRequirementHendler(ILogger<MinimumAgeRequirementHendler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            /// problem z claimem - pobierany jest uzytkownik bez claimow
            var test = context.User.FindFirst(x => x.Type == "DateOfBirth");
           var dateOfBirth =  DateTime.Parse(context.User.FindFirst(x => x.Type == "DateOfBirth").Value);
            var userEmail = context.User.FindFirst(y => y.Type == ClaimTypes.Name).Value;
            _logger.LogInformation($"User: {userEmail} with date of birth {dateOfBirth}");
            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation($"Authorization succedded");
                context.Succeed(requirement);                
            }
            else {
                _logger.LogInformation($"Authorization failed");
            }

            return Task.CompletedTask;
        }
        
    }
}
