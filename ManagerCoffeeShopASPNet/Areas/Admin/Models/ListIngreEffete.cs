using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject(Id = "ListIngreEffete")]
    public class ListIngreEffete
    {
        [JsonProperty(PropertyName = "IngredientEffete")]
        public List<IngredientEffete> IngredientEffete { get; set; }
    }
}