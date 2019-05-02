using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface ShipDetailDAO
    {

        bool InsertShipDetail(int ShipID, int OrderID, string CustName, string Address, string Phone, string Status);   
    }
}
