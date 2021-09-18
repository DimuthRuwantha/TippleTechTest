﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models.Response;
using Interface;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Services.Context;
using Services.utils;

namespace Services
{
    public class CocktailService : ICockTailService
    {
        private readonly HttpClient _http;
        private const string BaseAddress = "https://www.thecocktaildb.com/";
        private readonly ICocktailDataService _cockTailDataService;
        public CocktailService(HttpClient http, ICocktailDataService cockTailDataDataService)
        {
            _http = http;
            _cockTailDataService = cockTailDataDataService;
            _http.BaseAddress = new Uri(BaseAddress);
        }
        
        public async Task<CocktailList> SearchCockTail(string ingredient)
        {
            try
            {
                var cocktailsRaw = await _cockTailDataService.GetCocktailsByIngredient(ingredient);
                
                if (cocktailsRaw == null)
                {
                    return null;
                }
                var cocktails = await PopulateCocktailInfoById(cocktailsRaw);
            
                return cocktails;
            }
            catch (JsonException e)
            {
                //nothing to cast, JToken is empty
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                // other exceptions
                Console.WriteLine(e.Message);
                throw;
            }
            return null;
        }

        public async Task<Cocktail> GetRandomCockTail()
        {
            var drink = await _cockTailDataService.GetRandomCockTail();
            //await _http.GetFromJsonAsync<CockTails>($"/api/json/v1/1/random.php");
            if (drink == null) return null;
            var cockTail = MapToCocktailObject(drink);
            return cockTail;
        }

        private async Task<CocktailList> PopulateCocktailInfoById(CockTails cocktails)
        {
            var list = new List<int>();
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

            var fillUpDataTasks = cocktails.Drinks.Select(async drink =>
            {
                var drinkId = int.Parse(drink.idDrink);
                if (drinkId < cockTailList.meta.firstId) cockTailList.meta.firstId = drinkId;
                if (drinkId > cockTailList.meta.lastId) cockTailList.meta.lastId = drinkId;
                var drinkDetails = await _cockTailDataService.GetCocktailDrinkById(drinkId);
                
                if (drinkDetails != null)
                {
                    var cockTail = MapToCocktailObject(drinkDetails);
                    if (cockTail != null)
                    {
                        cockTailList.Cocktails.Add(cockTail);
                        list.Add(cockTail.Ingredients.Count);
                    }
                }
            });

            await Task.WhenAll(fillUpDataTasks);

            cockTailList.meta.medianIngredientCount = Median.GetMedian(list);
            
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
                    Ingredients = new List<string>(),
                    Instructions = drink.strInstructions,
                };

                var count = 1;
                do
                {
                    var prop = drink.GetType().GetProperty($"strIngredient{count}");
                    var value = (string)prop?.GetValue(drink, null);
                    if (value == null) break;
                    
                    cockTail.Ingredients.Add(value);
                    count++;
                } while (true);

                return cockTail;
            }
            catch (Exception e)
            {
                // Log Error
                // decide to throw or silently fail and continue
                Console.WriteLine(e);
            }
            return null;
        }
    }
}