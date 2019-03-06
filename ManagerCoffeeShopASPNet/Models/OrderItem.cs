using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class OrderItem
    {
        private int OrderID { get; set; }
        private int FDID { get; set; }
        private int Quantity { get; set; }
        private string Desc { get; set; }
        private string Status { get; set; }
    }
}