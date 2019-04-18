using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface AccountDAO
    {
        IEnumerable<Account> GetAllAccount();
        int GetLastUserID(); 
        Account GetAccountByEmail(string Email);
        Account GetAccountByUserName(string UserName);
        Account GetAccountByUserID(int UserID);
        bool InsertAccount(string UserName, string Password, string Email, string AccType, string Position, string Avatar);
        bool DeleteAccount(int UserID);
    }
}
