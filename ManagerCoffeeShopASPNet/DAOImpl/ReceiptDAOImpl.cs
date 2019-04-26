using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class ReceiptDAOImpl:ReceiptDAO
    {
        private CoffeeShopDBDataContext context;
        public ReceiptDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastReceiptID()
        {
            try
            {
                int ID = (from receipt in context.Receipts
                          orderby receipt.ReceiptID descending
                          select receipt.ReceiptID).FirstOrDefault();
                return ID;
            }
            catch(Exception ex)
            {
                throw new Exception("Error GetLastReceiptID " + ex.Message);
                return 0;
            }
        }
        public IEnumerable<Receipt> GetAllReceipt()
        {
            try
            {
                return context.Receipts.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public bool InsertReceipt(DateTime Date, double TotalAmount, string Currency, string Status)
        {
            try
            {
                Receipt receipt = new Receipt();
                receipt.Date = Date;
                receipt.TotalAmount = TotalAmount;
                receipt.Currency = Currency;
                receipt.Status = Status;
                this.context.Receipts.InsertOnSubmit(receipt);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error InsertReceipt" + ex.Message);
            }
        }
    }
}