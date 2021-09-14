using System;
using Common.Models.Response;
using Interface;

namespace Services
{
    public class CocktailService : ICockTailService
    {
        public CocktailList SearchCockTail(string ingredient)
        {
            return new CocktailList();
        }

        public Cocktail GetRandomCockTail()
        {
            return new Cocktail();
        }
    }
}