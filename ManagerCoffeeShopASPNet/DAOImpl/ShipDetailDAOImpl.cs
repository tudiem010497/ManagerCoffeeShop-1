using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class ShipDetailDAOImpl : ShipDetailDAO
    {
        private CoffeeShopDBDataContext context;
        public ShipDetailDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<ShipDetail> GetListShipDelivery()
        {
            return context.ShipDetails.ToList();
        }
        public IEnumerable<ShipDetail> GetShipDeliveryByStatus(string Status)
        {
            //string Status
            IEnumerable<ShipDetail> s = from ship in context.ShipDetails
                                        where ship.Status == Status /*|| ship.Status == "Close" || ship.Status == "Failed"*/
                                        select ship;
            return s;
        }
        public IEnumerable<ShipDetail> GetShipDeliveryWaitToConfirm()
        {
            IEnumerable<ShipDetail> s = from ship in context.ShipDetails
                                        where ship.Status == "Wait"
                                        select ship;
            return s;
        }
        public ShipDetail GetShipDetailByOrderID(int OrderID)
        {
            ShipDetail shipdetail = (from s in context.ShipDetails
                                     where s.OrderID == OrderID
                                     select s).Single();
            return shipdetail;
        }
        public ShipDetail GetShipDeliveryByShipDetailID(int ShipDetailID)
        {
            ShipDetail s = (from ship in context.ShipDetails
                            where ship.ShipDetailID == ShipDetailID
                            select ship).SingleOrDefault();
            return s;
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
            catch (Exception e)
            {
                throw new Exception("Error Insert ShipDetail: " + e.Message);
            }
        }
        public bool UpdateShipDetailStatusByShipDetailID(int ShipDetailID, string Status)
        {
            try
            {
                ShipDetail shipDetail = this.context.ShipDetails.Single(o => o.ShipDetailID == ShipDetailID);
                shipDetail.Status = Status;
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Update Ship Detail Status : " + ex.Message);
            }
        }
        public bool UpdateShipDetailStatus(int OrderID, string Status)
        {
            try
            {
                ShipDetail shipDetail = this.context.ShipDetails.Single(o => o.OrderID == OrderID);
                shipDetail.Status = Status;
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Update Ship Detail Status : " + ex.Message);
            }
        }
    }
}