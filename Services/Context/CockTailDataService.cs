using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Models.DTO;
using Common.Models.Response;
using Microsoft.Extensions.Configuration;

namespace Services.Context
{
    public class CockTailDataService : ICocktailDataService
    {
        private readonly HttpClient _http;
        private const string BaseAddress = "https://www.thecocktaildb.com/";
        
        public CockTailDataService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _http.BaseAddress = new Uri(configuration["BaseUrl"]);
        }
        public async Task<DrinksList> GetCocktailsByIngredient(string ingredient)
        {
            try
            {
                var cocktailsRaw = await _http.GetFromJsonAsync<DrinksList>($"api/json/v1/1/filter.php?i={ingredient}");
                return cocktailsRaw;
            }
            catch (JsonException e)
            {
                //nothing to cast, JToken is empty
                Console.WriteLine(e);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return null;
        }

        public async Task<Drink> GetCocktailDrinkById(int drinkId)
        {
            try
            {
                var details = await _http.GetFromJsonAsync<DrinksList>($"api/json/v1/1/lookup.php?i={drinkId}");
                return details?.Drinks.First();
            }
            catch (JsonException e)
            {
                //nothing to cast, JToken is empty
                Console.WriteLine(e);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return null;
        }

        public async Task<Drink> GetRandomCockTail()
        {
            try
            {
                var cockTailDrink =  await _http.GetFromJsonAsync<DrinksList>($"/api/json/v1/1/random.php");
                return cockTailDrink?.Drinks.First();
            }
            catch (JsonException e)
            {
                //nothing to cast, JToken is empty
                Console.WriteLine(e);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return null;
        }
    }
}