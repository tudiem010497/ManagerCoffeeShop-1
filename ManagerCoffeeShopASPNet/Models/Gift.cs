using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class Gift
    {
        private int GiftID { get; set; }
        private int SupplierID { get; set; }
        private string Name { get; set; }
        private float UnitPrice { get; set; }
        private string Currency { get; set; }
        private string Desc { get; set; }
    }
}