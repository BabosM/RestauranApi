using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation resourceOperation { get; set; }

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            this.resourceOperation = resourceOperation;
        }
    }
}
