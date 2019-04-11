using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class EmployeeDAOImpl:EmployeeDAO
    {
        private CoffeeShopDBDataContext context;
        public EmployeeDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public Employee GetEmployeeAccountByUserID(int UserID)
        {
            Employee em = (from employee in context.Employees
                           where employee.UserID == UserID
                           select employee).SingleOrDefault();
            return em;
        }
    }
}