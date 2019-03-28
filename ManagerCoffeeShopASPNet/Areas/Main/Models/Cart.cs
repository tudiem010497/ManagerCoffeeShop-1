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
        [JsonProperty(PropertyName = "Price")]
        public int Price { get; set; }
        [JsonProperty(PropertyName = "Total")]
        public int Total
        {
            get { return Quantity * Price; }
            set { }
        }
    }
}
