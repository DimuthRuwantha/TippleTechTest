using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Common.Models.Response;
using Interface;

namespace Services.Context
{
    public class CockTailDataService : ICocktailDataService
    {
        private readonly HttpClient _http;
        private const string BaseAddress = "https://www.thecocktaildb.com/";
        
        public CockTailDataService(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri(BaseAddress);
        }
        public async Task<CockTails> GetCocktailsByIngredient(string ingredient)
        {
            var cocktailsRaw = await _http.GetFromJsonAsync<CockTails>($"api/json/v1/1/filter.php?i={ingredient}");
            return cocktailsRaw;
        }

        public async Task<Drink> GetCocktailDrinkById(int drinkId)
        {
            var details = await _http.GetFromJsonAsync<CockTails>($"api/json/v1/1/lookup.php?i={drinkId}");

            return details?.Drinks.First();
        }

        public async Task<Drink> GetRandomCockTail()
        {
            var cockTailDrink =  await _http.GetFromJsonAsync<CockTails>($"/api/json/v1/1/random.php");
            return cockTailDrink?.Drinks.First();
        }
    }
}