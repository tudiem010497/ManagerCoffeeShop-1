using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class OrderPromotionDAOImpl:OrderPromotionDAO
    {
        CoffeeShopDBDataContext context;
        public OrderPromotionDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public bool InsertOrderPromotion(int PromotionID, int OrderID)
        {
            try
            {
                OrderPromotion orderPromotion = new OrderPromotion();
                orderPromotion.OrderID = OrderID;
                orderPromotion.PromotionID = PromotionID;
                context.OrderPromotions.InsertOnSubmit(orderPromotion);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Insert " + e.Message);
            }
        }
    }
}