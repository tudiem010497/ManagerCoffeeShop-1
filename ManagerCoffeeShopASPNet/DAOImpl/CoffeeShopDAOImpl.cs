using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class CoffeeShopDAOImpl : CoffeeShopDAO
    {
        private CoffeeShopDBDataContext context;
        public CoffeeShopDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<CoffeeShop> GetAllCoffeeShop()
        {
            return context.CoffeeShops.ToList();
        }
        public IEnumerable<CoffeeShop> GetCoffeeShopByCSID(int CSID)
        {
            IEnumerable<CoffeeShop> cs = (from coffeeshop in context.CoffeeShops
                                          where coffeeshop.CSID == CSID
                                          select coffeeshop);
            return cs;
        }
        public bool InsertCoffeeShop(string Name, string Address, string Phone, string LogoImagePath, string TitleAbout, string DescAbout, string TitleContact, string DescContact, string Email)
        {
            try
            {
                CoffeeShop cs = new CoffeeShop();
                cs.Name = Name;
                cs.Address = Address;
                cs.Phone = Phone;
                cs.LogoImagePath = LogoImagePath;
                cs.TitleAbout = TitleAbout;
                cs.DescAbout = DescAbout;
                cs.TitleContact = TitleContact;
                cs.DescContact = DescContact;
                cs.Email = Email;
                context.CoffeeShops.InsertOnSubmit(cs);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error insert coffee shop " + e.Message);
            }
        }
        public bool EditCoffeeShop(CoffeeShop cs)
        {
            try
            {
                CoffeeShop coffeeshop = context.CoffeeShops.FirstOrDefault(c => c.CSID == cs.CSID);
                coffeeshop.CSID = cs.CSID;
                coffeeshop.Name = cs.Name;
                coffeeshop.Address = cs.Address;
                coffeeshop.Phone = cs.Phone;
                coffeeshop.LogoImagePath = cs.LogoImagePath;
                coffeeshop.TitleAbout = cs.TitleAbout;
                coffeeshop.DescAbout = cs.DescAbout;
                coffeeshop.TitleContact = cs.TitleContact;
                coffeeshop.DescContact = cs.DescContact;
                coffeeshop.Email = cs.Email;
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error edit coffee shop " + e.Message);
            }
        }
    }
}