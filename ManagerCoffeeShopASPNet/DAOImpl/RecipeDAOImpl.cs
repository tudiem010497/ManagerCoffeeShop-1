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
        public int GetLastRecipeID()
        {
            try
            {
                int id = (from recipe in context.Recipes
                          orderby recipe.RecID descending
                          select recipe).FirstOrDefault().RecID;
                return id;
            }
            catch(Exception ex)
            {
                return 0;
            }
            
        }
        public Recipe GetRecipeByFoodDrinkID(int FoodAndDrinkID)
        {
            Recipe rec = this.context.Recipes.FirstOrDefault(o => o.FDID == FoodAndDrinkID);
            return rec;
        }
        public bool InsertRecipe(int FDID)
        {
            try
            {
                int RecipeID = GetLastRecipeID() + 1;
                Recipe recipe = new Recipe();
                recipe.RecID = RecipeID;
                recipe.FDID = FDID;
                context.Recipes.InsertOnSubmit(recipe);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error InsertRecipe" + ex.Message);
            }
            
        }
       
    }
}