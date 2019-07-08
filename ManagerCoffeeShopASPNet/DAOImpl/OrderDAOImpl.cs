using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class OrderDAOImpl : OrderDAO
    {
        CoffeeShopDBDataContext context;
        public OrderDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastID()
        {
            try
            {
                int id = (from order
                     in context.Orders
                          orderby order.OrderID descending
                          select order.OrderID).FirstOrDefault();
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool InsertOrder(int PosID, DateTime OrderDateTime, DateTime PaidDateTime,
            double TotalAmount, string Currency, string Desc, string Status)
        {
            try
            {
                Order order = new Order();
                order.OrderID = this.GetLastID() + 1;
                order.PosID = PosID;
                order.OrderDateTime = OrderDateTime;
                order.PaidDateTime = PaidDateTime;
                order.TotalAmount = TotalAmount;
                order.Currency = Currency;
                order.Desc = Desc;
                order.Status = Status;
                // context.ExecuteCommand("SET IDENTITY_INSERT dbo.Order ON");
                context.Orders.InsertOnSubmit(order);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Inser To Order : " + ex.Message));
                return false;
            }
        }
        public bool InsertOrderWithoutPosID(DateTime OrderDateTime, DateTime PaidDateTime,
           double TotalAmount, string Currency, string Desc, string Status)
        {
            try
            {
                Order order = new Order();
                order.OrderID = this.GetLastID() + 1;
                order.OrderDateTime = OrderDateTime;
                order.PaidDateTime = PaidDateTime;
                order.TotalAmount = TotalAmount;
                order.Currency = Currency;
                order.Desc = Desc;
                order.Status = Status;
                // context.ExecuteCommand("SET IDENTITY_INSERT dbo.Order ON");
                context.Orders.InsertOnSubmit(order);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Inser To Order : " + ex.Message));
            }
        }
        public IEnumerable<Order> GetAllOrderByStatus(string Status)
        {
            IEnumerable<Order> orders = from order in context.Orders
                                        where order.Status == Status
                                        orderby order.OrderDateTime ascending
                                        select order;
            return orders;
        }
        public Order GetOrderByStatus(string Status)
        {
            Order order = (from o in context.Orders
                           where o.Status == Status
                           orderby o.OrderDateTime ascending
                           select o).Single();
            return order;
        }
        public IEnumerable<Order> GetAllOrderByDescAndStatus(string Desc, string Status)
        {
            IEnumerable<Order> orders = from order in context.Orders
                                        where order.Desc == Desc && order.Status == Status
                                        orderby order.OrderDateTime ascending
                                        select order;
            return orders;
        }
        public bool UpdateOrderStatus(int OrderID, string Status)
        {
            try
            {
                Order order = this.context.Orders.Single(o => o.OrderID == OrderID);
                order.Status = Status;
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error UpdateOrderStatus : " + ex.Message);
            }
        }
        public IEnumerable<Order> GetAllOrderPendingOrReady()
        {
            IEnumerable<Order> orders = from order in this.context.Orders
                                        where order.Status == "Pending"
                                        || (order.Status == "Ready")
                                        orderby order.OrderDateTime ascending
                                        select order;
            return orders;
        }
        public IEnumerable<Order> GetAllOrder()
        {
            IEnumerable<Order> orders = from order in context.Orders
                                        select order;
            return orders;
        }
        public IEnumerable<Order> GetOrderByOrderID(int OrderID)
        {
            IEnumerable<Order> o = from order in context.Orders
                                   where OrderID == order.OrderID
                                   select order;
            return o;
        }
    }
}