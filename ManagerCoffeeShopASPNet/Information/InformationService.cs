using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationService
    {
        private FoodAndDrinkDAO _foodAndDrinkDAO;
        public InformationService()
        {
            this._foodAndDrinkDAO = (FoodAndDrinkDAO)new FoodAndDrinkDAOImpl();
        }
        public IEnumerable<FoodAndDrink> GetFoodAndDrink()
        {
            return _foodAndDrinkDAO.GetAllFoodAndDrink();
        }
        public FoodAndDrink GetFoodAndDrinkByID(int id)
        {
            return _foodAndDrinkDAO.GetFoodAndDrinkByID(id);
        }
    }
}