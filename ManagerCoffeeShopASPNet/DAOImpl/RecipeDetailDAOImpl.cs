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
        public int GetLastRecipeDetailID()
        {
            int id = (from recipeDetail in context.RecipeDetails
                      orderby recipeDetail.RecipeDetailID descending
                      select recipeDetail).FirstOrDefault().RecipeDetailID;
            return id;
        }
        public IEnumerable<RecipeDetail> GetAllRecipeDetailByRecipeID(int RecipeID)
        {
            IEnumerable<RecipeDetail> recipeDetails = from recipedetail in this.context.RecipeDetails
                                                          where recipedetail.RecID == RecipeID
                                                          orderby recipedetail.Step ascending
                                                          select recipedetail;
            return recipeDetails;
        }
        public bool InsertRecipeDetail(int RecID, int Step, int IngreID, double Amount, string Unit, string Desc)
        {
            try
            {
                int RecipeDetailID = GetLastRecipeDetailID() + 1;
                RecipeDetail recipeDetail = new RecipeDetail();
                recipeDetail.RecipeDetailID = RecipeDetailID;
                recipeDetail.Step = Step;
                recipeDetail.IngreID = IngreID;
                recipeDetail.Amount = Amount;
                recipeDetail.Unit = Unit;
                recipeDetail.Desc = Desc;
                this.context.RecipeDetails.InsertOnSubmit(recipeDetail);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error InsertRecipeDetail : " + ex.Message);
            }
        }
    }
}