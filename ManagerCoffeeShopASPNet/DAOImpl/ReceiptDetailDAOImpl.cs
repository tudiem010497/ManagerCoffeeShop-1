﻿using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class ReceiptDetailDAOImpl:ReceiptDetailDAO
    {
        private CoffeeShopDBDataContext context;
        public ReceiptDetailDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<ReceiptDetail> GetReceiptDetailByReceiptID(int ReceiptID)
        {
            try
            {
                IEnumerable<ReceiptDetail> list = from receiptdetail in context.ReceiptDetails
                                                  where receiptdetail.ReceiptID == ReceiptID
                                                  select receiptdetail;
                return list;                                                  
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public ReceiptDetail GetReceiptDetailByReceiptDetailID(int ReceiptDetailID)
        {
            try
            {
                ReceiptDetail list = (from receiptdetail in context.ReceiptDetails
                                                  where receiptdetail.ReceiptDetailID==ReceiptDetailID
                                                  select receiptdetail).SingleOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool InsertReceiptDetail(int ReceiptID, int IngreID, double Amount, string Unit, double UnitPrice, string Currency, string Status)
        {
            try
            {
                ReceiptDetail detail = new ReceiptDetail();
                detail.ReceiptID = ReceiptID;
                detail.IngreID = IngreID;
                detail.Amount = Amount;
                detail.Unit = Unit;
                detail.UnitPrice = UnitPrice;
                detail.Currency = Currency;
                detail.Status = Status;
                context.ReceiptDetails.InsertOnSubmit(detail);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error InsertReceiptDetailByReceiptID" + ex.Message);
            }
        }
        public bool UpdateReceiptDetail(int ReceiptDetailID, string Status)
        {
            try
            {
                ReceiptDetail receiptDetail = context.ReceiptDetails.FirstOrDefault(m => m.ReceiptDetailID == ReceiptDetailID);
                receiptDetail.Status = Status;
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Update ReceiptDetail: " + e.Message);
            }
        }
    }
}