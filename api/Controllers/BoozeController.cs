using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models.Response;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class BoozeController : ControllerBase
    {
        // We will use the public CocktailDB API as our backend
        // https://www.thecocktaildb.com/api.php
        //
        // Bonus points
        // - Speed improvements
        // - Unit Tests
        private readonly ICockTailService _cockTailService;

        public BoozeController(ICockTailService cockTailService)
        {
            _cockTailService = cockTailService;
        }
        
        [HttpGet]
        [Route("search-ingredient/{ingredient}")]
        public async Task<IActionResult> GetIngredientSearch([FromRoute] string ingredient)
        {
            try
            {
                // TODO - Search the CocktailDB for cocktails with the ingredient given, and return the cocktails
                // https://www.thecocktaildb.com/api/json/v1/1/filter.php?i=Gin
            
                var cocktailList1 =  await _cockTailService.SearchCockTail(ingredient);
                // You will need to populate the cocktail details from
                // https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i=11007
                // The calculate and fill in the meta object

                if (cocktailList1 == null || !cocktailList1.Cocktails.Any()) return NoContent();
            
                return Ok(cocktailList1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("random")]
        public async Task<IActionResult> GetRandom()
        {
            try
            {
                // TODO - Go and get a random cocktail
                // https://www.thecocktaildb.com/api/json/v1/1/random.php
                var cocktail = await _cockTailService.GetRandomCockTail();

                if (cocktail == null) NoContent();
                return Ok(cocktail);
            }
            catch (Exception e)
            {
                // handle errors if time permits
                Console.WriteLine(e);
                return StatusCode(500);
            }
            
        }
    }
}