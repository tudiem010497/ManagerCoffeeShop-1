using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationBatender
    {
        private OrderDAO _orderDAO;
        private OrderItemDAO _orderItemDAO;
        public InformationBatender()
        {
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
        }
        public IEnumerable<Order> GetAllOrderByStatus(string status)
        {
            return this._orderDAO.GetAllOrderByStatus(status);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderID(int OrderID)
        {
            return this._orderItemDAO.GetAllOrderItemByOrderID(OrderID);
        }
        public bool UpdateStatus(int orderItemID, string status)
        {
            return this._orderItemDAO.UpdateOrderItemStatus(orderItemID, status);
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndStatus(int OrderID, string Status)
        {
            return this._orderItemDAO.GetAllOrderItemByOrderIDAndStatus(OrderID, Status);
        }
        public bool UpdateOrderStatus(int OrderID, string Status)
        {
            return this._orderDAO.UpdateOrderStatus(OrderID, Status);
        }
    }
}