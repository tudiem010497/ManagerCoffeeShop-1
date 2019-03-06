using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class Ingredients
    {
        private int IngreID { get; set; }
        private int SupplierID { get; set; }
        private string Name { get; set; }
        private float Amount { get; set; }
        private string Unit { get; set; }
    }
}