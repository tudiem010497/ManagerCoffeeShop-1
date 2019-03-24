using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationAccountant
    {
        private OrderDAO _orderDAO;
        private OrderItemDAO _orderItemDAO;
        private FoodAndDrinkDAO _foodAndDrinkDAO;
        public InformationAccountant()
        {
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
        }

        /// <summary>
        /// Tính tổng hóa đơn (chưa tính khuyến mãi)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public double CalculateTotalAmount(int OrderID)
        {
            IEnumerable<OrderItem> orderItems = this._orderItemDAO.GetAllOrderItemByOrderID(OrderID);
            double totalAmount = 0;
            foreach (OrderItem item in orderItems)
            {
                if (item.Status == "Closed")
                {
                    totalAmount = totalAmount + item.Quantity * this._foodAndDrinkDAO.GetFoodAndDrinkByID(item.FDID).UnitPrice;
                }
            }
            return totalAmount;
        }
    }
}