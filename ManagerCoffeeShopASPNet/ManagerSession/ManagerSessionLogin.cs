using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ManagerCoffeeShopASPNet.ManagerSession
{
    public class ManagerSessionLogin
    {
        private AccountDAO _accountDAO;
        private EmployeeDAO _employeeDAO;
        private CustomerDAO _customerDAO;
        public ManagerSessionLogin()
        {
            this._accountDAO = (AccountDAO)new AccountDAOImpl();
            this._employeeDAO = (EmployeeDAO)new EmployeeDAOImpl();
            this._customerDAO = (CustomerDAO)new CustomerDAOImpl();
        }
        public Account GetCurrentUser()
        {
            HttpSessionState session = HttpContext.Current.Session;
            object obj = session["email"];
            if(obj != null)
            {
                string email = obj.ToString();
                return this._accountDAO.GetAccountByEmail(email);
            }
            return null;
        }
        public Employee GetCurrentEmployee()
        {
            Account acc = GetCurrentUser();
            if(acc != null)
            {
                if(acc.AccType != "Customer")
                {
                    int UserID = acc.UserID;
                    return this._employeeDAO.GetEmployeeAccountByUserID(UserID);
                }
            }
            return null;
        }
        public Customer GetCurrentCustomer()
        {
            Account acc = GetCurrentUser();
            if(acc.AccType == "Customer")
            {
                int UserID = acc.UserID;
                return this._customerDAO.GetCustomerByUserID(UserID);
            }
            return null;
        }
    }
}