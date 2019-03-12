using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class FoodAndDrinkDAOImpl:FoodAndDrinkDAO
    {
        CoffeeShopDBDataContext context;
        public FoodAndDrinkDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<FoodAndDrink> GetAllFoodAndDrink()
        {
            return context.FoodAndDrinks.ToList();
        }
        public FoodAndDrink GetFoodAndDrinkByID(int id)
        {
            var fd = from foodAndDrink in context.FoodAndDrinks where foodAndDrink.FDID == id select foodAndDrink;
            return fd.ToArray().ElementAt(0);
        }
    }
}