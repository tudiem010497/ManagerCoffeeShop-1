﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface PromotionDAO
    {
        IEnumerable<Promotion> GetAllPromotion();
        int GetLastPromotionID();
        Promotion GetPromotionByID(int PromotionID);
        bool InsertPromotion(string Name, string Desc, DateTime StartDate, DateTime EndDate, string TypePromotion, float Discount, float MinOrderTotalAmount);
        bool EditPromotion(Promotion promotion);
        IEnumerable<Promotion> GetPromotionByDateTime();
    }
}