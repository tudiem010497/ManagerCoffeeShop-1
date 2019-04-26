using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject(Id ="ReceiptModel")]
    public class ReceiptModel
    {
        [JsonProperty(PropertyName ="SupplierID")]
        public int SupplierID { get; set; }
        [JsonProperty(PropertyName ="ReceiptDetailModel")]
        public List<ReceiptDetailModel> ReceiptDetailModel { get; set; }

    }
}