using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class CoffeeShopDAOImpl:CoffeeShopDAO
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
            catch(Exception e)
            {
                throw new Exception("Error insert coffee shop " + e.Message);
            }
        }
    }
}