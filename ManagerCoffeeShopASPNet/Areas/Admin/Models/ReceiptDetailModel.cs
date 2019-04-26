using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject("ReceiptDetailModel")]
    public class ReceiptDetailModel
    {
        [JsonProperty(PropertyName = "IngreID")]
        public int IngreID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName ="Quantity")]
        public double Quantity { get; set; }
        [JsonProperty(PropertyName ="TotalAmount")]
        public double TotalAmount { get; set; }
    }
}