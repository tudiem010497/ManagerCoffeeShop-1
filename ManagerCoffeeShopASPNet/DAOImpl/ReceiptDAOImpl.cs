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
        public IEnumerable<Receipt> GetReceiptWaitToConfirm()
        {
            try
            {
                string status = "Waiting";
                IEnumerable<Receipt> r = from receipt in context.Receipts
                                         where receipt.Status == status
                                         select receipt;
                return r;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public IEnumerable<Receipt> GetReceiptByReceiptID(int ReceiptID)
        {
            try
            {
                IEnumerable<Receipt> r = from receipt in context.Receipts
                                         where receipt.ReceiptID==ReceiptID
                                         select receipt;
                return r;
            }
            catch (Exception e)
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
        public bool UpdateReceipt(int ReceiptID, string Status)
        {
            try
            {
                Receipt receipt = context.Receipts.FirstOrDefault(m => m.ReceiptID == ReceiptID);
                receipt.Status = Status;
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Update Receipt" + ex.Message);
            }
        }
        public Receipt GetReceiptByID(int ReceiptID)
        {
            try
            {
                Receipt r = (from receipt in context.Receipts
                             where receipt.ReceiptID == ReceiptID
                             select receipt).Single();
                return r;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}