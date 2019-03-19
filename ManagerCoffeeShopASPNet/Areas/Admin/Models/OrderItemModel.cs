using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject(Id = "OrderItemModel")]
    public class OrderItemModel
    {
        [JsonProperty(PropertyName = "FoodAndDrinkID")]
        public int FoodAndDrinkID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Quantity")]
        public int Quantity { get; set; }
    }


}