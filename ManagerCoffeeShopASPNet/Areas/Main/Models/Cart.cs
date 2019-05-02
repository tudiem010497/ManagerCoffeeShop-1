using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.Areas.Main.Models
{
    [JsonObject]
    public class Cart
    {
        [JsonProperty(PropertyName = "FDID")]
        public int FDID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Quantity")]
        public int Quantity { get; set; }
        [JsonProperty(PropertyName = "Desc")]
        public string Desc { get; set; }
        [JsonProperty(PropertyName = "Price")]
        public int Price { get; set; }
        [JsonProperty(PropertyName = "Total")]
        public int Total
        {
            get { return Quantity * Price; }
            set { }
        }
        [JsonProperty(PropertyName = "UserID")]
        public int UserID { get; set; }
        [JsonProperty(PropertyName = "CustName")]
        public string CustName { get; set; }
        [JsonProperty(PropertyName = "Tel")]
        public string Tel { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }
    }
}
