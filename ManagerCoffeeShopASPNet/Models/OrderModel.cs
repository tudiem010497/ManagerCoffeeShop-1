using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class OrderModel
    {
        private int PosID { get; set; }
        private DateTime OrderDateTime { get; set; }
        private IEnumerable<OrderItem> OrderItem { get; set; }
    }
}