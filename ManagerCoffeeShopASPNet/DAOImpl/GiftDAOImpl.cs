using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class GiftDAOImpl : GiftDAO
    {
        CoffeeShopDBDataContext context;
        public GiftDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastGiftID()
        {
            try
            {
                int id = (from gift in context.Gifts orderby gift.GiftID descending select gift.GiftID).FirstOrDefault();
                return id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public IEnumerable<Gift> GetAllGift()
        {
            return context.Gifts.ToList();
        }
        public Gift GetGiftByID(int GiftID)
        {
            var g = (from gift in context.Gifts
                     where gift.GiftID == GiftID
                     select gift).SingleOrDefault();
            return g;
        }
        public IEnumerable<Gift> GetGiftByGiftID(int GiftID)
        {
            IEnumerable<Gift> g = from gift in context.Gifts
                                  where gift.GiftID == GiftID
                                  select gift;
            return g;
        }
        public bool InsertGift(int SupplierID, string Name, float UnitPrice, string Currency, string Desc)
        {
            try
            {
                int GiftID = GetLastGiftID() + 1;
                Gift g = new Gift();
                g.SupplierID = SupplierID;
                g.Name = Name;
                g.UnitPrice = UnitPrice;
                g.Currency = Currency;
                g.Desc = Desc;
                context.Gifts.InsertOnSubmit(g);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error insert gift " + e.Message);
            }
        }
        public bool EditGift(Gift gift)
        {
            try
            {
                Gift g = context.Gifts.FirstOrDefault(p => p.GiftID == gift.GiftID);
                g.GiftID = gift.GiftID;
                g.SupplierID = gift.SupplierID;
                g.Name = gift.Name;
                g.UnitPrice = gift.UnitPrice;
                g.Currency = gift.Currency;
                g.Desc = gift.Desc;
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error edit gift " + e.Message);
            }
        }
        public bool DeleteGift(int GiftID)
        {
            try
            {
                Gift gift = GetGiftByID(GiftID);
                context.Gifts.DeleteOnSubmit(gift);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error delete gift " + e.Message);
            }
        }
    }
}