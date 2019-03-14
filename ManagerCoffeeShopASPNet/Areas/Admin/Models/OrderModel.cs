using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Models
{
    public class OrderModel
    {
        private int PosID { get; set; }
        private List<OrderItem> orderItems { get; set; }
    }
}