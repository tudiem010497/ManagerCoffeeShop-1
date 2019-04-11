using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class AccountDAOImpl:AccountDAO
    {
        private CoffeeShopDBDataContext context;
        public AccountDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext(); 
        }
        public IEnumerable<Account>  GetAllAccount()
        {
            return context.Accounts.ToList();
        }
        public Account GetAccountByEmail(string Email)
        {
            Account acc = (from account in context.Accounts
                       where account.Email == Email
                       select account).SingleOrDefault();
            return acc;
        }
    }
}