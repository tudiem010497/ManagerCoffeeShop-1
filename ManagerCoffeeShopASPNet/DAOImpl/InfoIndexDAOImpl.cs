using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class InfoIndexDAOImpl: InfoIndexDAO
    {
        CoffeeShopDBDataContext context;
        public InfoIndexDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<InfoIndex> GetInfoIndex()
        {
            return context.InfoIndexes.ToList();
        }
    }
}