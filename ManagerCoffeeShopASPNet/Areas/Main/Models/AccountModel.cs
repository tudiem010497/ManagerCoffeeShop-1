using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ManagerCoffeeShopASPNet.Areas.Main.Models
{
    public class AccountModel
    {
        private CoffeeShopDBDataContext context = null;
        public AccountModel()
        {
            context = new CoffeeShopDBDataContext();
        }
        public bool Login(string UserName, string Password)
        {
            bool? res = false;
            context.sp_Account_Login_Check(UserName, Password, ref res);
            return (res ?? false);
        }
    }
}
