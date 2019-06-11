using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    [JsonObject(Id = "Diagram")]
    public class DiagramUpdate
    {
        [JsonProperty(PropertyName = "CLSID")]
        public int CLSID { get; set; }

        [JsonProperty(PropertyName = "widthDiagram")]
        public float widthDiagram { get; set; }
        [JsonProperty(PropertyName = "heightDiagram")]
        public float heightDiagram { get; set; }
        [JsonProperty(PropertyName = "ratioDiagram")]
        public float ratioDiagram { get; set; }
        [JsonProperty(PropertyName = "ListImageDiagram")]
        public List<ImageDiagram> ListImageDiagram { get; set; }
        [JsonProperty(PropertyName = "FloorID")]
        public string FloorID { get; set; }
        [JsonProperty(PropertyName = "CSID")]
        public int CSID { get; set; }
    }
}