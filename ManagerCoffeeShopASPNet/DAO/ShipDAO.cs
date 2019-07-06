using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface ShipDAO
    {
        Ship GetShipByCustName(string CustName);
        bool InsertShip(int EmpolyeeID, string CustName, DateTime ShipDate);
        bool InsertShipWithUserID(int EmpolyeeID, int UserID, string CustName, DateTime ShipDate);
        Ship GetShipByShipID(int ShipID);
        int GetLastShipID();
    }
}