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
        public int GetLastShipID()
        {
            try
            {
                int ShipID = (from ships in context.Ships
                 orderby ships.ShipID descending
                 select ships.ShipID
                 ).FirstOrDefault();
                return ShipID;
            }
            catch
            {
                return 0;
            }
        }

        public Ship GetShipByCustName(string CustName)
        {
            Ship ship = (from ships in context.Ships
                         where ships.CustName == CustName
                         select ships).SingleOrDefault();
            return ship;
        }

        public Ship GetShipByShipID(int ShipID)
        {
            try
            {
                Ship ship = (from ships in context.Ships
                             where ships.ShipID == ShipID
                             select ships).SingleOrDefault();
                return ship;
            }
            catch
            {
                return null;
            }
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