using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class ESTIngredient
    {
        private int IngreID { get; set; }
        private string Name { get; set; }
        private float ReceiptAmount { get; set; }
        private float ESTAmount { get; set; }
        private string Unit { get; set; }
        private DateTime Date { get; set; }
    }
}