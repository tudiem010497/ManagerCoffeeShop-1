using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class RecipeDAOImpl: RecipeDAO
    {
        CoffeeShopDBDataContext context;
        public RecipeDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public Recipe GetRecipeByFoodDrinkID(int FoodAndDrinkID)
        {
            Recipe rec = this.context.Recipes.FirstOrDefault(o => o.FDID == FoodAndDrinkID);
            return rec;
        }
    }
}