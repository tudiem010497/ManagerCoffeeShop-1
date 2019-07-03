using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class PromotionDAOImpl: PromotionDAO
    {
        CoffeeShopDBDataContext context;
        public PromotionDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Promotion> GetAllPromotion()
        {
            return context.Promotions.ToList();
        }
        public int GetLastPromotionID()
        {
            try
            {
                int id = (from p in context.Promotions orderby p.PromotionID descending select p.PromotionID).FirstOrDefault();
                return id;
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        public Promotion GetPromotionByID(int PromotionID)
        {
            var p = (from promotion in context.Promotions
                     where promotion.PromotionID == PromotionID
                     select promotion).SingleOrDefault();
            return p;
        }
        public bool InsertPromotion (string Name, string Desc, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                int PromotionID = GetLastPromotionID() + 1;
                Promotion p = new Promotion();
                p.Name = Name;
                p.Desc = Desc;
                p.StartDate = StartDate;
                p.EndDate = EndDate;
                context.Promotions.InsertOnSubmit(p);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error insert promotion " + e.Message);
            }
        }
        public bool EditPromotion(Promotion promotion)
        {
            try
            {
                Promotion promo = context.Promotions.FirstOrDefault(p => p.PromotionID == promotion.PromotionID);
                promo.PromotionID = promotion.PromotionID;
                promo.Name = promotion.Name;
                promo.Desc = promotion.Desc;
                promo.StartDate = promotion.StartDate;
                promo.EndDate = promotion.EndDate;
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error edit promotion " + e.Message);
            }
        }
        public IEnumerable<Promotion> GetPromotionByDateTime()
        {
            var p = from promotion in context.Promotions
                    where DateTime.Now >= promotion.StartDate && DateTime.Now <= promotion.EndDate
                    select promotion;
            return p;
        }
    }
}