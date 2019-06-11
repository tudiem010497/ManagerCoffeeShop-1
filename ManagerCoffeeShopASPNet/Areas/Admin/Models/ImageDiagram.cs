using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject(Id = "ImageDiagram")]
    public class ImageDiagram
    {
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "Href")]
        public string Href { get; set; }
        [JsonProperty(PropertyName = "x")]
        public float x { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float y { get; set; }
        [JsonProperty(PropertyName = "width")]
        public float width { get; set; }
        [JsonProperty(PropertyName = "height")]
        public float height { get; set; }
        [JsonProperty(PropertyName = "rotate")]
        public int rotate { get; set; }

    }
}