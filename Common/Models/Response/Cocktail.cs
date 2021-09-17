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

    public class CockTails
    {
        [DataMember]
        public List<Drink> Drinks { get; set; }
    }

    public class Drink
    {
        public string strDrink { get; set; }
        
        public string strDrinkThumb { get; set; }
        
        public string idDrink { get; set; }

        public string strInstructions { get; set; }

        public string strIngredient1 { get; set; }
        public string strMeashure1 { get; set; }
        public string strImageSource { get; set; }
        
    }
}