using ManagerCoffeeShopASPNet.DAO;
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
            double Amount, string Unit, double UnitPrice, string Currency)
        {
            return this._ingredientDAO.EditIngredient(IngreID, SupplierID, Name, Amount, Unit, UnitPrice, Currency);
        }
        public bool InsertIngredient(int SupplierID, string Name,
            double Amount, string Unit, double UnitPrice, string Currency)
        {
            return this._ingredientDAO.InsertIngredient(SupplierID, Name, Amount, Unit, UnitPrice, Currency);
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
    }
}