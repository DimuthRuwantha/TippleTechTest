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
            var cocktailsRaw = await _http.GetFromJsonAsync<CockTails>($"api/json/v1/1/filter.php?i=Gin");
            
            if (cocktailsRaw == null) return null;
            var cocktails = await FillMetaData(cocktailsRaw);
            
            return cocktails;
        }

        public async Task<Cocktail> GetRandomCockTail()
        {
            var c = await _http.GetFromJsonAsync<CockTails>($"/api/json/v1/1/random.php");
            return new Cocktail();
        }

        private async Task<CocktailList> FillMetaData(CockTails cocktails)
        {
            var cockTailList = new CocktailList()
            {
                meta = new ListMeta()
                {
                    firstId = int.MaxValue,
                    lastId = int.MinValue,
                    count = cocktails.Drinks.Count,
                }
            };
           
            foreach (var drink in cocktails.Drinks)
            {
                //https://stackoverflow.com/questions/15136542/parallel-foreach-with-asynchronous-lambda
                var drinkId = int.Parse(drink.idDrink);
                if(drinkId < cockTailList.meta.firstId) cockTailList.meta.firstId = drinkId;
                if(drinkId > cockTailList.meta.lastId) cockTailList.meta.lastId = drinkId;
                var details = await _http.GetFromJsonAsync<CockTails>($"api/json/v1/1/lookup.php?i={drink.idDrink}");
            }

            return cockTailList;
        }
    }
}