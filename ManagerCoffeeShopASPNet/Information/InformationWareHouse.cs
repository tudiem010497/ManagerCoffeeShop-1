﻿using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationWareHouse
    {
        private IngredientDAO _ingredientDAO;
        private SupplierDAO _supplierDAO;
        private ReceiptDAO _receiptDAO;
        private ReceiptDetailDAO _receiptDetailDAO;
        public InformationWareHouse()
        {
            this._ingredientDAO = (IngredientDAO)new IngredientDAOImpl();
            this._supplierDAO = (SupplierDAO)new SupplierDAOImpl();
            this._receiptDAO = (ReceiptDAO)new ReceiptDAOImpl();
            this._receiptDetailDAO = (ReceiptDetailDAO)new ReceiptDetailDAOImpl();
        }

        public IEnumerable<Ingredient> GetAllIngredientEffete()
        {
            return this._ingredientDAO.GetAllIngredientEffete();
        }
        public IEnumerable<Ingredient> GetAllIngredient()
        {
            return this._ingredientDAO.GetAllIngredient();
        }
        public Ingredient GetIngredientByIngreID(int IngreID)
        {
            return this._ingredientDAO.GetIngredientByIngreID(IngreID);
        }
        public IEnumerable<Supplier> GetAllSupplier()
        {
            return this._supplierDAO.GetAllSupplier();
        }
        public bool EditIngredient(int IngreID, int SupplierID, string Name,
            double Amount, double AmountMin, string Unit, double UnitPrice, string Currency)
        {
            return this._ingredientDAO.EditIngredient(IngreID, SupplierID, Name, Amount, AmountMin, Unit, UnitPrice, Currency);
        }
        public bool InsertIngredient(int SupplierID, string Name,
            double Amount, double AmountMin, string Unit, double UnitPrice, string Currency)
        {
            return this._ingredientDAO.InsertIngredient(SupplierID, Name, Amount, AmountMin, Unit, UnitPrice, Currency);
        }
        public Supplier GetSupplierBySupplierID(int SupplierID)
        {
            return this._supplierDAO.GetSupplierBySupplierID(SupplierID);
        }
        public bool EditSupplier(int SupplierID, string Name, string Address, string Phone)
        {
            return this._supplierDAO.EditSupplier(SupplierID, Name, Address, Phone);
        }
        public bool InsertSupplier(string Name, string Address, string Phone)
        {
            return this._supplierDAO.InsertSupplier(Name, Address, Phone);
        }
        public IEnumerable<Receipt> GetAllReceipt()
        {
            return this._receiptDAO.GetAllReceipt();
        }
        public IEnumerable<ReceiptDetail> GetAllReceiptDetailByReceiptID(int ReceiptID)
        {
            return this._receiptDetailDAO.GetReceiptDetailByReceiptID(ReceiptID);
        }
        public bool InsertReceipt(DateTime Date, double TotalAmount, string Currency, string Status)
        {
            return this._receiptDAO.InsertReceipt(Date, TotalAmount, Currency, Status);
        }
        public bool InsertReceiptDetail(int ReceiptID, int IngreID, int GiftID, double Amount, string Unit, double UnitPrice, string Currency, string Status)
        {
            return this._receiptDetailDAO.InsertReceiptDetail(ReceiptID, IngreID, GiftID, Amount, Unit, UnitPrice, Currency, Status);
        }
        public int GetLastReceiptID()
        {
            return this._receiptDAO.GetLastReceiptID();
        }
        public bool InsertReceiptDetailMissIngreID(int ReceiptID, int GiftID, double Amount, double UnitPrice, string ReferenceDesc, string Currency, string Status)
        {
            return this._receiptDetailDAO.InsertReceiptDetailMissIngreID(ReceiptID, GiftID, Amount, UnitPrice, ReferenceDesc, Currency, Status);
        }
        public bool InsertReceiptDetailMissGiftID(int ReceiptID, int IngreID, double Amount, string Unit, double UnitPrice, string ReferenceDesc, string Currency, string Status)
        {
            return this._receiptDetailDAO.InsertReceiptDetailMissGiftID(ReceiptID, IngreID, Amount, Unit, UnitPrice, ReferenceDesc, Currency, Status);
        }
        public bool UpdateReceiptDetail(int ReceiptDetailID, string Status)
        {
            return this._receiptDetailDAO.UpdateReceiptDetail(ReceiptDetailID, Status);
        }
        public ReceiptDetail GetReceiptDetailByReceiptDetailID(int ReceiptDetailID)
        {
            return this._receiptDetailDAO.GetReceiptDetailByReceiptDetailID(ReceiptDetailID);
        }
        public bool UpdateAmountIngredient(int IngreID, double Amount)
        {
            return this._ingredientDAO.UpdateAmountIngredient(IngreID, Amount);
        }
        public bool UpdateReceipt(int ReceiptID, string Status)
        {
            return this._receiptDAO.UpdateReceipt(ReceiptID, Status);
        }
        public bool UpdateReceiptDetailByReceiptID(int ReceiptID, string Status)
        {
            return this._receiptDetailDAO.UpdateReceiptDetailByReceiptID(ReceiptID, Status);
        }
        public Receipt GetReceiptByID(int ReceiptID)
        {
            return this._receiptDAO.GetReceiptByID(ReceiptID);
        }
    }
}