using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class IngreExchange
    {
        private int IngreID { get; set; }
        private int FDID { get; set; }
        private float Amount { get; set; }
        private string Unit { get; set; }
        private int Quantity { get; set; }
    }
}