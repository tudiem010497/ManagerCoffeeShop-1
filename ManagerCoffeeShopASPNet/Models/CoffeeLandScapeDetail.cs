using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class CoffeeLandScapeDetail
    {
        private int IteamID { get; set; }
        private int CLSID { get; set; }
        private int ShapeID { get; set; }
        private int RowID { get; set; }
        private int ColID { get; set; }
        private float Width { get; set; }
        private float Lenghth { get; set; }
        private string ItemType { get; set; }
    }
}