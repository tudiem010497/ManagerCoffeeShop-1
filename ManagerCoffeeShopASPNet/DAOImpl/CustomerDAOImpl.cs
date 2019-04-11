using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class CustomerDAOImpl:CustomerDAO
    {
        private CoffeeShopDBDataContext context;
        public CustomerDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public Customer GetCustomerByUserID(int UserID)
        {
            Customer cust = (from customer in context.Customers
                             where customer.UserID == UserID
                             select customer).SingleOrDefault();
            return cust;
        }
    }
}