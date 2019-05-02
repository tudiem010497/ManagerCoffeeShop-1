using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class ShipDAOImpl : ShipDAO
    {
        private CoffeeShopDBDataContext context;
        public ShipDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public Ship GetShipByCustName(string CustName)
        {
            Ship ship = (from ships in context.Ships
                         where ships.CustName == CustName
                         select ships).SingleOrDefault();
            return ship;
        }
        public bool InsertShip(int EmpolyeeID, string CustName, DateTime ShipDate)
        {
            try
            {
                Ship ship = new Ship();
                ship.EmployeeID = EmpolyeeID;
                ship.CustName = CustName;
                ship.ShipDate = ShipDate;
                context.Ships.InsertOnSubmit(ship);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error Insert Ship: " + e.Message);
            }
        }
        public bool InsertShipWithUserID(int EmpolyeeID, int UserID, string CustName, DateTime ShipDate)
        {
            try
            {
                Ship ship = new Ship();
                ship.EmployeeID = EmpolyeeID;
                ship.CustomerID = UserID;
                ship.CustName = CustName;
                ship.ShipDate = ShipDate;
                context.Ships.InsertOnSubmit(ship);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error Insert Ship: " + e.Message);
            }
        }
    }
}