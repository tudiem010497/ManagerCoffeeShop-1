using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class OrderItemDAOImpl:OrderItemDAO
    {
        CoffeeShopDBDataContext context;
        public OrderItemDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastID()
        {
            try
            {
                int id = (from orderItem
                      in context.OrderItems
                          orderby orderItem.OrderItemID descending
                          select orderItem.OrderItemID
                      ).FirstOrDefault();
                return id;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public void InsertOrderItem(int OrderID, int FDID, int Quantity, string Desc, string Status)
        {
            try
            {
                int id = GetLastID() + 1;
                OrderItem orderitem = new OrderItem();
                orderitem.OrderItemID = id;
                orderitem.OrderID = OrderID;
                orderitem.FDID = FDID;
                orderitem.Quantity = Quantity;
                orderitem.Desc = Desc;
                orderitem.Status = Status;
                context.ExecuteCommand("SET IDENTITY_INSERT dbo.OrderItem ON");
                context.OrderItems.InsertOnSubmit(orderitem);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Inser To OrderItem" + ex.Message);
            }

        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderID(int id)
        {
            IEnumerable<OrderItem> orderitems = from orderitem in context.OrderItems
                                                where orderitem.OrderID == id
                                                select orderitem;
            return orderitems;
        }
        public OrderItem GetOrderItemByID(int ID)
        {
            OrderItem orderItem = context.OrderItems.Single(o => o.OrderItemID == ID);
            return orderItem;
        }
        public OrderItem GetOrderItemByOrderID(int OrderID)
        {
            OrderItem orderItem = context.OrderItems.Single(o => o.OrderID == OrderID);
            return orderItem;
        }
        public bool UpdateOrderItemStatus(int orderItemID, string status)
        {
            try
            {
                OrderItem orderItem = context.OrderItems.Single(o => o.OrderItemID == orderItemID);
                orderItem.Status = status;
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error OrderItemDAOImpl : " + ex.Message);
                return false;
            }
        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndStatus(int OrderID, string Status)
        {
            IEnumerable<OrderItem> orderItems = from orderItem in context.OrderItems
                                                where (orderItem.OrderID == OrderID
                                                && orderItem.Status == Status)
                                                select orderItem;
            return orderItems;

        }
        public IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndNeedService(int OrderID)
        {
            IEnumerable<OrderItem> orderItems = from orderItem in context.OrderItems
                                                where (orderItem.OrderID == OrderID)
                                                &&( (orderItem.Status == "Ready")
                                                || (orderItem.Status == "Pending")
                                                || (orderItem.Status == "Cancel"))
                                                select orderItem;
            return orderItems;
        }
        public IEnumerable<OrderItem> GetAllOrderItemNeedService()
        {
            IEnumerable<OrderItem> orderItems = from orderItem in context.OrderItems
                                                where orderItem.Status == "Ready"
                                                || orderItem.Status == "Cancel"
                                                select orderItem;
            return orderItems;
        }
        public IEnumerable<OrderItem> GetAllOrderItemByStatus(string status)
        {
            IEnumerable<OrderItem> orderItems = from orderItem in context.OrderItems
                                                where orderItem.Status == status
                                                select orderItem;
            return orderItems;
        }
        public IEnumerable<OrderItem> GetAllOrderItem()
        {
            IEnumerable<OrderItem> orderItems = from orderitem in context.OrderItems
                                                select orderitem;
            return orderItems;
        }
    }
    
}