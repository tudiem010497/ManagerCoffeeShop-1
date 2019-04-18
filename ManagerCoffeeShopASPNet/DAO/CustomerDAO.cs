using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface CustomerDAO
    {
        Customer GetCustomerByUserID(int UserID);
        bool InsertCustomer(int UserID, string Name, DateTime DOB, string Address, string IdentityNum, string Phone);
    }
}
