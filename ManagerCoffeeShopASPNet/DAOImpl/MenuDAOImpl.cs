using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class MenuDAOImpl:MenuDAO
    {
        CoffeeShopDBDataContext context;
        public MenuDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Menu> getAllMenu()
        {
            return this.context.Menus.ToList();
        }
    }
}