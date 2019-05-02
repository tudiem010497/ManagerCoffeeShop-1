using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class ShipDetailDAOImpl:ShipDetailDAO
    {
        private CoffeeShopDBDataContext context;
        public ShipDetailDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public bool InsertShipDetail(int ShipID, int OrderID, string CustName, string Address, string Phone, string Status)
        {
            try
            {
                ShipDetail shipdetail = new ShipDetail();
                shipdetail.ShipID = ShipID;
                shipdetail.OrderID = OrderID;
                shipdetail.CustName = CustName;
                shipdetail.Address = Address;
                shipdetail.Phone = Phone;
                shipdetail.Status = Status;
                context.ShipDetails.InsertOnSubmit(shipdetail);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Insert ShipDetail: " + e.Message);
            }
        }
    }
}