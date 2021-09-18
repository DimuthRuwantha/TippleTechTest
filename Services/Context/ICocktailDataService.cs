using System.Threading.Tasks;
using Common.Models.DTO;
using Common.Models.Response;

namespace Services.Context
{
    public interface ICocktailDataService
    {
        Task<DrinksList> GetCocktailsByIngredient(string ingredient);
        Task<Drink> GetCocktailDrinkById(int drinkId);
        Task<Drink> GetRandomCockTail();
    }
}