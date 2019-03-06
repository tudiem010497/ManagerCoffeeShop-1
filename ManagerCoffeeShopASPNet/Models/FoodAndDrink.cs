using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class FoodAndDrink
    {
        private int FDID { get; set; }
        private string Name { get; set; }
        private string Desc { get; set; }
        private string Size { get; set; }
        private string Type { get; set; }
        private float UnitPrice { get; set; }
        private string Currency { get; set; }
    }
}