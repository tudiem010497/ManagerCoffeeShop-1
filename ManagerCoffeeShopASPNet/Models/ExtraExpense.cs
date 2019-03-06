using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class ExtraExpense
    {
        private int ExpenseID { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
        private string Type { get; set; }
        private int Preiod { get; set; }
        private string Desc { get; set; }
        private float Price { get; set; }
        private string Currency { get; set; }

    }
}