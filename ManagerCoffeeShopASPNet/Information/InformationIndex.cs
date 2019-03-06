using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationIndex
    {
        AccountDAO accDAO;
        FoodAndDrinkDAO foodAndDrinkDAO;
        public InformationIndex()
        {
            // this.accDAO = (AccountDAO)new AccountDAOImpl();
            this.foodAndDrinkDAO = (FoodAndDrinkDAO)new FoodAndDrinkDAOImpl();
        }
        public IEnumerable<FoodAndDrink> GetInformationFoodAndDrink(){
            //IEnumerable<Account> accounts = accDAO.GetAllAccount();
            //return accounts;
            IEnumerable<FoodAndDrink> fds = foodAndDrinkDAO.GetAllFoodAndDrink();
            return fds;
        }

    }
}