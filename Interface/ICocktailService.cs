using System;
using Common.Models.Response;

namespace Interface
{
    public interface ICockTailService
    {
       CocktailList SearchCockTail(string ingredient);

       Cocktail GetRandomCockTail();
    }
}