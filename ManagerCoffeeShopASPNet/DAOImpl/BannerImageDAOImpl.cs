using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class BannerImageDAOImpl: BannerImageDAO
    {
        CoffeeShopDBDataContext context;
        public BannerImageDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<BannerImage> GetAllBannerImage()
        {
            return context.BannerImages.ToList();
        }
    }
}