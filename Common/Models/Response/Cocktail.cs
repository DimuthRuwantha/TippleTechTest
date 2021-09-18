using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Common.Models.Response
{
    [DataContract]
    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public List<string> Ingredients { get; set; }
        public string ImageURL { get; set; }
    }

    #region Temp objects
    public class DrinksList
    {
        public List<Drink> Drinks { get; set; }
    }

    public class Drink
    {
        public string strDrink { get; set; }

        public string idDrink { get; set; }

        public string strInstructions { get; set; }

        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }
        
        public string strImageSource { get; set; }
        
    }
    #endregion
}