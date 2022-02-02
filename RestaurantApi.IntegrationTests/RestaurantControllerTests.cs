using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
//Uruchomienie Web api w sekcji arrange aby bylo mozliwe wyslanie zapytania klientem HTTP z kodu
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net.Http;
using RestaurantApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace RestaurantApi.IntegrationTests
{
    public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public RestaurantControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {
                        // usuniecie instacji db service
                        var dbContextOption = services
                           .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));

                       services.Remove(dbContextOption);
                        // ustawienie dbContext na baze danych InMemory
                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
                   });
               }).CreateClient();

        }

        [Theory]
        [InlineData("pageSize=5&pageNumber=1")]
        [InlineData("pageSize=10&pageNumber=1")]
        [InlineData("pageSize=20&pageNumber=1")]
        
        public async Task GetAll_WithQueryParameters_ReturnsOkResult(string queryParam)
        {
           // update - przeniesiony do konstruktora. Współdzielony kontekst wątków
            // konstruktor przyjmuje paramentr generyczny  ktorym jest Entry Point. W tym przypadku jest to Startup.
            //var factory = new WebApplicationFactory<Startup>();
            // za pomoca tego klienta bede mogl sie odwolac do metod z api. 
           // var client = factory.CreateClient();
           

            var response = await _client.GetAsync("/api/restaurant?" + queryParam);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            
        }
        [Theory]
        [InlineData("pageSize=51&pageNumber=1")]
        [InlineData("pageSize=110&pageNumber=1")]  
        [InlineData(null)]  
        public async Task GetAll_WithInvalidQueryParameters_ReturnsBanResult(string queryParam)
        {

            var response = await _client.GetAsync("/api/restaurant?" + queryParam);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
