using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject(Id = "OrderModel")]
    public class OrderModel
    {
        [JsonProperty(PropertyName = "Desc")]
        public string Desc { get; set; }
        [JsonProperty(PropertyName = "PosID")]
        public int PosID { get; set; }
        [JsonProperty(PropertyName = "PromotionID")]
        public int PromotionID { get; set; }
        [JsonProperty(PropertyName = "OrderItemModel")]
        public List<OrderItemModel> OrderItemModel { get; set; }
    }
}