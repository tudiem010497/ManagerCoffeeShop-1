using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationService
    {
        private FoodAndDrinkDAO _foodAndDrinkDAO;
        private OrderDAO _orderDAO;
        private OrderItemDAO _orderItemDAO;
        private OrderPromotionDAO _orderPromotionDAO;
        private PositionDAO _positionDAO;
        private AccountDAO _accountDAO;
        private ShipDAO _shipDAO;
        private ShipDetailDAO _shipDetailDAO;
        public InformationService()
        {
            this._foodAndDrinkDAO = (FoodAndDrinkDAO)new FoodAndDrinkDAOImpl();
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
            this._orderPromotionDAO = (OrderPromotionDAO)new OrderPromotionDAOImpl();
            this._positionDAO = (PositionDAO)new PositionDAOImpl();
            this._accountDAO = (AccountDAO)new AccountDAOImpl();
            this._shipDAO = (ShipDAO)new ShipDAOImpl();
            this._shipDetailDAO = (ShipDetailDAO)new ShipDetailDAOImpl();
        }
        public IEnumerable<FoodAndDrink> GetFoodAndDrink()
        {
            return _foodAndDrinkDAO.GetAllFoodAndDrink();
        }
        public FoodAndDrink GetFoodAndDrinkByID(int id)
        {
            return _foodAndDrinkDAO.GetFoodAndDrinkByID(id);
        }
        public bool InsertOrder(int PosID, DateTime OrderDateTime, DateTime PaidDateTime,
            double TotalAmount, string Currency, string Desc, string Status)
        {
            return _orderDAO.InsertOrder(PosID, OrderDateTime, PaidDateTime,
           TotalAmount, Currency, Desc, Status);
        }
        public bool InsertOrderWithoutPosID(DateTime OrderDateTime, DateTime PaidDateTime,
            double TotalAmount, string Currency, string Desc, string Status)
        {
            return _orderDAO.InsertOrderWithoutPosID(OrderDateTime, PaidDateTime,
           TotalAmount, Currency, Desc, Status);
        }
        public void InsertOrderItem(int OrderID, int FDID, int Quantity, string Desc, string Status)
        {
            _orderItemDAO.InsertOrderItem(OrderID, FDID, Quantity, Desc, Status);
        }
        public IEnumerable<Position> GetAllPosition()
        {
            return _positionDAO.GetAllPosition();
        }
        public int GetLastOrderIDID()
        {
            return _orderDAO.GetLastID();
        }

        public IEnumerable<Order> GetAllOrderByStatus(string status)
        {
            return this._orderDAO.GetAllOrderByStatus(status);
        }
        public IEnumerable<Order> GetAllOrderByDescAndStatus(string Desc, string Status)
        {
            return this._orderDAO.GetAllOrderByDescAndStatus(Desc, Status);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderID(int OrderID)
        {
            return this._orderItemDAO.GetAllOrderItemByOrderID(OrderID);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndStatus(int OrderID, string status)
        {
            return this._orderItemDAO.GetAllOrderItemByOrderIDAndStatus(OrderID, status);
        }
        public bool UpdateOrderItemStatus(int OrderItemID,string status)
        {
            return this._orderItemDAO.UpdateOrderItemStatus(OrderItemID, status);
        }
        public bool UpdateOrderStatus(int OrderID, string status)
        {
            return this._orderDAO.UpdateOrderStatus(OrderID, status);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndNeedService(int OrderID)
        {
            return this._orderItemDAO.GetAllOrderItemByOrderIDAndNeedService(OrderID);
        }
        public OrderItem GetOrderItemByOrderItemID(int OrderItemID)
        {
            return this._orderItemDAO.GetOrderItemByID(OrderItemID);
        }
        public IEnumerable<Order> GetAllOrderPendingOrReady()
        {
            return this._orderDAO.GetAllOrderPendingOrReady();
        }
        public IEnumerable<OrderItem> GetAllOrderItemNeedService()
        {
            return this._orderItemDAO.GetAllOrderItemNeedService();
        }
        public IEnumerable<OrderItem> GetAllOrderItem()
        {
            return this._orderItemDAO.GetAllOrderItem();
        }
        public IEnumerable<Order> GetAllOrder()
        {
            return this._orderDAO.GetAllOrder();
        }
        public IEnumerable<Order> GetOrderByOrderID(int OrderID)
        {
            return this._orderDAO.GetOrderByOrderID(OrderID);
        }
        public Account GetAccountByUserID(int UserID)
        {
            return _accountDAO.GetAccountByUserID(UserID);
        }
        public bool InsertShip(int EmployeeID, string CustName, DateTime ShipDate)
        {
            return this._shipDAO.InsertShip(EmployeeID, CustName, ShipDate);
        }
        public bool InsertShipWithUserID(int EmpolyeeID, int UserID, string CustName, DateTime ShipDate)
        {
            return this._shipDAO.InsertShipWithUserID(EmpolyeeID, UserID, CustName, ShipDate);
        }
        public Ship GetShipByCustName(string CustName)
        {
            return this._shipDAO.GetShipByCustName(CustName);
        }
        public IEnumerable<ShipDetail> GetListShipDelivery()
        {
            return this._shipDetailDAO.GetListShipDelivery();
        }
        public IEnumerable<ShipDetail> GetShipDeliveryByStatus()
        {
            return this._shipDetailDAO.GetShipDeliveryByStatus();
        }
        public ShipDetail GetShipDeliveryByShipDetailID(int ShipDetailID)
        {
            return this._shipDetailDAO.GetShipDeliveryByShipDetailID(ShipDetailID);
        }
        public bool InsertShipDetail(int ShipID, int OrderID, string CustName, string Address, string Phone, string Status)
        {
            return this._shipDetailDAO.InsertShipDetail(ShipID, OrderID, CustName, Address, Phone, Status);
        }
        public bool UpdateShipDetailStatus(int OrderID, string Status)
        {
            return this._shipDetailDAO.UpdateShipDetailStatus(OrderID, Status);
        }
        public bool UpdateShipDetailStatusByShipDetailID(int ShipDetailID, string Status)
        {
            return this._shipDetailDAO.UpdateShipDetailStatusByShipDetailID(ShipDetailID, Status);
        }
        public bool InsertOrderPromotion(int PromotionID, int OrderID)
        {
            return this._orderPromotionDAO.InsertOrderPromotion(PromotionID, OrderID);
        }
    }
}