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
        [JsonProperty(PropertyName = "ReferenceDesc")]
        public string ReferenceDesc { get; set; }
        [JsonProperty(PropertyName = "GiftID")]
        public int GiftID { get; set; }
        [JsonProperty(PropertyName = "Name_gift")]
        public string Name_gift { get; set; }
        [JsonProperty(PropertyName = "Quantity_gift")]
        public double Quantity_gift { get; set; }
        [JsonProperty(PropertyName = "TotalAmount_gift")]
        public double TotalAmount_gift { get; set; }

        [JsonProperty(PropertyName = "ReferenceDesc_gift")]
        public string ReferenceDesc_gift { get; set; }
    }
}