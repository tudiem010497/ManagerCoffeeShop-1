using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Models
{
    public class Employee
    {
        private int EmployeeID { get; set; }
        private int CSID { get; set; }
        private string Name { get; set; }
        private string Address { get; set; }
        private string Phone { get; set; }
    }
}