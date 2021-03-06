using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Common.Models.Response
{
    [DataContract]
    public class CocktailList
    {
        [DataMember]
        [JsonPropertyName("drinks")]
        public List<Cocktail> Cocktails { get; set; }
        public ListMeta meta { get; set; }
    }

    public class ListMeta
    {
        public int count { get; set; }    // number of results
        public int firstId { get; set; }    // smallest Id of the results
        public int lastId { get; set; }    // largest Id of the results
        public double medianIngredientCount { get; set; }    // median of the number of ingredients per cocktail
    }
}