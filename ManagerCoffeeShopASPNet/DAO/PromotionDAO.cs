using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface PromotionDAO
    {
        IEnumerable<Promotion> GetAllPromotion();
        int GetPromotionID();
        Promotion GetPromotionByID(int PromotionID);
        bool InsertPromotion(string Name, string Desc, DateTime StartDate, DateTime EndDate);
        bool EditPromotion(Promotion promotion);
    }
}