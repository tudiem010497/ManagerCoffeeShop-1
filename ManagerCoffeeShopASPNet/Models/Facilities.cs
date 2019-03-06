using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class Facilities
    {
        private int FacilitiesID { get; set; }
        private int SupplierID { get; set; }
        private string Name { get; set; }
        private int Quantity { get; set; }
        private string Unit { get; set; }
        private float UnitPrice { get; set; }
        private string Currency { get; set; }
    }
}