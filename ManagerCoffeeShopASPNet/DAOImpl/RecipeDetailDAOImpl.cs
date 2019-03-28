using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class RecipeDetailDAOImpl:RecipeDetailDAO
    {
        CoffeeShopDBDataContext context;
        public RecipeDetailDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<RecipeDetail> GetAllRecipeDetailByRecipeID(int RecipeID)
        {
            IEnumerable<RecipeDetail> recipeDetails = from recipedetail in this.context.RecipeDetails
                                                      where recipedetail.RecID == RecipeID
                                                      orderby recipedetail.Step ascending
                                                      select recipedetail;
            return recipeDetails;
        }
    }
}