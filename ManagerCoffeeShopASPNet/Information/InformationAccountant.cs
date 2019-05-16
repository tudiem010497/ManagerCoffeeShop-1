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
        private ReceiptDAO _receiptDAO;
        private ReceiptDetailDAO _receiptDetailDAO;
        public InformationAccountant()
        {
            this._orderDAO = (OrderDAO)new OrderDAOImpl();
            this._orderItemDAO = (OrderItemDAO)new OrderItemDAOImpl();
            this._receiptDAO = (ReceiptDAO)new ReceiptDAOImpl();
            this._receiptDetailDAO = (ReceiptDetailDAO)new ReceiptDetailDAOImpl();
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

        public IEnumerable<Receipt> GetAllReceipt()
        {
            return this._receiptDAO.GetAllReceipt();
        }
        public IEnumerable<Receipt> GetReceiptWaitToConfirm()
        {
            return this._receiptDAO.GetReceiptWaitToConfirm();
        }
        public IEnumerable<Receipt> GetReceiptByReceiptID(int ReceiptID)
        {
            return this._receiptDAO.GetReceiptByReceiptID(ReceiptID);
        }
        public IEnumerable<ReceiptDetail> GetReceiptDetailByReceiptID(int ReceiptID)
        {
            return this._receiptDetailDAO.GetReceiptDetailByReceiptID(ReceiptID);
        }
        public ReceiptDetail GetReceiptDetailByReceiptDetailID(int ReceiptDetailID)
        {
            return this._receiptDetailDAO.GetReceiptDetailByReceiptDetailID(ReceiptDetailID);
        }
        public bool UpdateReceipt(int ReceiptID, string Status)
        {
            return this._receiptDAO.UpdateReceipt(ReceiptID, Status);
        }
        public bool UpdateReceiptDetail(int ReceiptDetailID, string Status)
        {
            return this._receiptDetailDAO.UpdateReceiptDetail(ReceiptDetailID, Status);
        }
    }
}