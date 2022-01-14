using FluentValidation;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 20 };
        private string[] allowedSortByColumn = new string[] { nameof(Restaurant.Name), nameof(Restaurant.Description), nameof(Restaurant.Category)};
        public RestaurantQueryValidator()
        {       
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
             .Custom((value, context) =>
             {
                 var isProperPageSize = allowedPageSizes.Contains(value);
                 if (!isProperPageSize)
                 {
                     context.AddFailure("PageSize", $"PageSize must in [{string.Join("," ,allowedPageSizes)}]");
                 }
             });

            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumn.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumn)}]");                             
        }
    }
}
