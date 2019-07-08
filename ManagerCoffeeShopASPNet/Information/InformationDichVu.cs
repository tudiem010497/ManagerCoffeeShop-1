using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationDichVu
    {
        private PromotionDAO _promotionDAO;
        private GiftDAO _giftDAO;
        public InformationDichVu()
        {
            this._promotionDAO = (PromotionDAO)new PromotionDAOImpl();
            this._giftDAO = (GiftDAO)new GiftDAOImpl();
        }
        public IEnumerable<Promotion> GetAllPromotion()
        {
            return this._promotionDAO.GetAllPromotion();
        }
        
        public Promotion GetPromotionByID(int PromotionID)
        {
            return this._promotionDAO.GetPromotionByID(PromotionID);
        }
        public bool InsertPromotion(string Name, string Desc, DateTime StartDate, DateTime EndDate, string TypePromotion, float Discount, float MinOrderTotalAmount)
        {
            return this._promotionDAO.InsertPromotion(Name, Desc, StartDate, EndDate, TypePromotion, Discount, MinOrderTotalAmount);
        }
        public bool EditPromotion(Promotion promotion)
        {
            return this._promotionDAO.EditPromotion(promotion);
        }
        public IEnumerable<Gift> GetAllGift()
        {
            return this._giftDAO.GetAllGift();
        }
        public Gift GetGiftByID(int GiftID)
        {
            return this._giftDAO.GetGiftByID(GiftID);
        }
        public IEnumerable<Gift> GetGiftByGiftID(int GiftID)
        {
            return this._giftDAO.GetGiftByGiftID(GiftID);
        }
        public bool InsertGift(int SupplierID, string Name, float UnitPrice, string Currency, string Desc)
        {
            return this._giftDAO.InsertGift(SupplierID, Name, UnitPrice, Currency, Desc);
        }
        public bool EditGift(Gift gift)
        {
            return this._giftDAO.EditGift(gift);
        }
        public bool DeleteGift(int GiftID)
        {
            return this._giftDAO.DeleteGift(GiftID);
        }
        public IEnumerable<Promotion> GetPromotionByDateTime()
        {
            return this._promotionDAO.GetPromotionByDateTime();
        }
    }
}