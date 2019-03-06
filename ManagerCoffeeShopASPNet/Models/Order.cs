using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class Order
    {
        private int OrderID { get; set; }
        private int CustomerID { get; set; }
        private int PosID { get; set; }
        private DateTime OrderDateTime { get; set; }
        private DateTime PaidDateTime { get; set; }
        private float DiscountMoney { get; set; }
        private float TotalAmount { get; set; }
        private string Currency { get; set; }
        private string Desc { get; set; }
        private string Status { get; set; }
    }
}