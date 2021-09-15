using System;
using System.Threading.Tasks;
using Common.Models.Response;

namespace Interface
{
    public interface ICockTailService
    {
        Task<CocktailList> SearchCockTail(string ingredient);

       Task<Cocktail> GetRandomCockTail();
    }
}