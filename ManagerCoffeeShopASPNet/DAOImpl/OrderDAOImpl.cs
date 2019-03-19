using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class OrderDAOImpl:OrderDAO
    {
        CoffeeShopDBDataContext context;
        public OrderDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastID()
        {
            int id = (from order
                     in context.Orders
                      orderby order.OrderID descending
                      select order.OrderID).FirstOrDefault();
            return id;
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
        public IEnumerable<Order> GetAllOrderByStatus(string Status)
        {
            IEnumerable<Order> orders = from order in context.Orders where order.Status == Status select order;
            return orders;
        }
    }
}