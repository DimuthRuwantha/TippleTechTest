using System;
using Common.Models.Response;
using Interface;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services
{
    public class CocktailService : ICockTailService
    {
        private readonly HttpClient _http;
        public CocktailService(HttpClient http)
        {
            _http = http;
        }
        public async Task<CocktailList> SearchCockTail(string ingredient)
        {
            
            var c = await _http.GetFromJsonAsync<CockTails>($"api/json/v1/1/filter.php?i=Gin");
            
            return new CocktailList();
        }

        public async Task<Cocktail> GetRandomCockTail()
        {
            var c = await _http.GetFromJsonAsync<CockTails>($"/api/json/v1/1/random.php");
            return new Cocktail();
        }
    }
}