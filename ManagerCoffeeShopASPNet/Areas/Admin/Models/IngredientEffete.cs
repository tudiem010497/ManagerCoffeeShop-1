using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject("IngredientEffete")]
    public class IngredientEffete
    {
        [JsonProperty(PropertyName = "IngreID")]
        public int IngreID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "UnitPrice")]
        public double UnitPrice { get; set; }
    }
}