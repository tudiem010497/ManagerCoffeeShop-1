using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class SupplierDAOImpl:SupplierDAO
    {
        private CoffeeShopDBDataContext context;
        public SupplierDAOImpl()
        {
            context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Supplier> GetAllSupplier()
        {
            return context.Suppliers.ToList();
        }
        public Supplier GetSupplierBySupplierID(int SupplierID)
        {
            Supplier sup = (from supplier in context.Suppliers
                            where supplier.SupplierID == SupplierID
                            select supplier).SingleOrDefault();
            return sup;
        }
        public bool EditSupplier(int SupplierID, string Name, string Address, string Phone)
        {
            try
            {
                Supplier sup = (from supplier in context.Suppliers
                                where supplier.SupplierID == SupplierID
                                select supplier).SingleOrDefault();
                sup.Name = Name;
                sup.Address = Address;
                sup.Phone = Phone;
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool InsertSupplier(string Name, string Address, string Phone)
        {
            try
            {
                Supplier sup = new Supplier();
                sup.Name = Name;
                sup.Address = Address;
                sup.Phone = Phone;
                context.Suppliers.InsertOnSubmit(sup);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                 return false;
            }
        }
    }
}