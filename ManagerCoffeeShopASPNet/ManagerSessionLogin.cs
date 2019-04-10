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
        public ManagerSessionLogin()
        {
            this._accountDAO = (AccountDAO)new AccountDAOImpl();
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
    }
}