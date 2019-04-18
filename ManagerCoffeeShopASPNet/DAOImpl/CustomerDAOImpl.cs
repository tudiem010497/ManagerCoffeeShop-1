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
        public bool InsertCustomer(int UserID, string Name, DateTime DOB, string Address, string IdentityNum, string Phone)
        {
            try
            {
                Customer cust = new Customer();
                cust.UserID = UserID;
                cust.Name = Name;
                cust.DOB = DOB;
                cust.Address = Address;
                cust.IdentityNum = IdentityNum;
                cust.Phone = Phone;
                cust.Point = 0;
                context.Customers.InsertOnSubmit(cust);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error insert customer " + e.Message);
            }
        }
    }
}