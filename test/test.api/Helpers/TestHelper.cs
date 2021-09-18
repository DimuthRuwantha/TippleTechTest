using System.Collections.Generic;
using Common.Models.Response;

namespace Test.api.Helpers
{
    public static class TestHelper
    {
        public static Cocktail CreatACockTail()
        {
            return new Cocktail()
            {
                Id = 16041,
                Name = "Mudslinger",
                Ingredients = new List<string>() {  
                    "Southern Comfort",
                    "Orange juice",
                    "Pepsi Cola"},
                Instructions = "Add all contents to a large jug or punch bowl. Stir well!",
            };
        }

        public static CocktailList CreateACocktailList()
        {
            return new CocktailList()
            {
                meta = new ListMeta()
                {
                    count = 3,
                    firstId = 1,
                    lastId = 3,
                    medianIngredientCount = 3,
                },
                Cocktails = new List<Cocktail>()
                {
                    new Cocktail()
                    {
                        Id = 1,
                        Name = "Mudslinger",
                        Ingredients = new List<string>()
                        {
                            "Southern Comfort",
                            "Orange juice",
                            "Pepsi Cola"
                        },
                        Instructions = "Add all contents to a large jug or punch bowl. Stir well!",
                    },
                    new Cocktail()
                    {
                        Id = 2,
                        Name = "50/50",
                        Ingredients = new List<string>()
                        {
                            "Vodka",
                            "Creme de Banane",
                            "Orange juice"
                        },
                        Instructions = "fill glass with crushed ice. Add vodka. Add a splash of grand-marnier. Fill with o.j.",
                    },
                    new Cocktail()
                    {
                        Id = 3,
                        Name = "Amaretto Stone Sour",
                        Ingredients = new List<string>()
                        {
                            "Amaretto",
                            "Sour mix",
                            "Orange juice"
                        },
                        Instructions = "Shake and Serve over ice",
                    },
                }
            };
        }
    }
}