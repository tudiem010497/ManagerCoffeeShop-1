using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class Customer
    {
        private int CustomerID { get; set; }
        private string Name { get; set; }
        private DateTime DOB { get; set; }
        private string Address { get; set; }
        private string IndentityNum { get; set; }
        private string Phone { get; set; }
        private int Point { get; set; }
    }
}