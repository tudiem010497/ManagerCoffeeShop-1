using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ManagerCoffeeShopASPNet.Information;

namespace ManagerCoffeeShopASPNet.Areas.Main.Models
{
    public class AccountModel
    {
        private CoffeeShopDBDataContext context = null;
        private InformationIndex info = new InformationIndex();
        public AccountModel()
        {
            context = new CoffeeShopDBDataContext();
        }
        public bool Login(string Email, string Password)
        {
            bool? res = false;
            context.sp_Account_Login_Check(Email, Password, ref res);
            return (res ?? false);
        }
        public int InsertCustomer(string Name, string Email, string Password)
        {
            if (Name == null || Email == null || Password == null)
                return 0;
            else
            {
                bool? res = false;
                //Account acc = info.GetAccountByEmail(Email);
                context.sp_Account_Email_Check(Email, ref res);
                if (res == true) return 0;
                else
                {
                    context.sp_INSERT_ACCOUNT_CUSTOMER(Name, Email, Password, "Customer");
                    return 1;
                }
            }
        }
        
    }
}
