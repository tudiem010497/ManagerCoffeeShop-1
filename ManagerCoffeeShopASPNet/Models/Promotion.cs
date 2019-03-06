using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class Promotion
    {
        private int PromotionID { get; set; }
        private string Name { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
        private string Desc { get; set; }
    }
}