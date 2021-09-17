using System;
using System.Collections.Generic;
using System.Linq;
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
                Cocktails = new List<Cocktail>(),
                meta = new ListMeta()
                {
                    firstId = int.MaxValue,
                    lastId = int.MinValue,
                    count = cocktails.Drinks.Count,
                }
            };
           
            /*foreach (var drink in cocktails.Drinks)
            {
                //https://stackoverflow.com/questions/15136542/parallel-foreach-with-asynchronous-lambda
                var drinkId = int.Parse(drink.idDrink);
                if(drinkId < cockTailList.meta.firstId) cockTailList.meta.firstId = drinkId;
                if(drinkId > cockTailList.meta.lastId) cockTailList.meta.lastId = drinkId;
                var details = await _http.GetFromJsonAsync<CockTails>($"api/json/v1/1/lookup.php?i={drink.idDrink}");
            }*/

            var fillUpDataTasks = cocktails.Drinks.Select(async drink =>
            {
                var drinkId = int.Parse(drink.idDrink);
                if (drinkId < cockTailList.meta.firstId) cockTailList.meta.firstId = drinkId;
                if (drinkId > cockTailList.meta.lastId) cockTailList.meta.lastId = drinkId;
                var details = await _http.GetFromJsonAsync<CockTails>($"api/json/v1/1/lookup.php?i={drink.idDrink}");

                if (details?.Drinks.First() != null)
                {
                    var drink1 = details?.Drinks?.First();
                    Console.WriteLine(drink1.idDrink);
                    var cockTail = MapToCocktailObject(drink1);
                    if(cockTail != null)cockTailList.Cocktails.Add(cockTail);
                }
            });

            await Task.WhenAll(fillUpDataTasks);
            return cockTailList;
        }

        private Cocktail MapToCocktailObject(Drink drink)
        {
            try
            {
                var cockTail = new Cocktail()
                {
                    Id = int.Parse(drink.idDrink),
                    Name = drink.strDrink,
                    ImageURL = drink.strImageSource,
                    Ingredients = new List<string>()
                    {
                        drink.strIngredient1,
                        //..
                    },
                    Instructions = drink.strInstructions
                };
                
                return cockTail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}