using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface OrderDAO
    {
        int GetLastID();
        void InsertOrder(int PosID, DateTime OrderDateTime,
            float TotalAmount, string Currency, string Desc, string Status);
    }
}
