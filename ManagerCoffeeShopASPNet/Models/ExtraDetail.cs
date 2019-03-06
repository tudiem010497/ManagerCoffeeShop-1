using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class ExtraDetail
    {
        private int ExpenseID { get; set; }
        private DateTime Hour { get; set; }
        private string Day { get; set; }
        private DateTime SpecificDate { get; set; }
        private int IngreID { get; set; }
        private int FDID { get; set; }
        private int FacilitiesID { get; set; }
        private float Amount { get; set; }
        private string Unit { get; set; }
    }
}