using System.Threading.Tasks;
using Common.Models.Response;

namespace Services.Context
{
    public interface ICocktailDataService
    {
        Task<CockTails> GetCocktailsByIngredient(string ingredient);
        Task<Drink> GetCocktailDrinkById(int drinkId);
        Task<Drink> GetRandomCockTail();
    }
}