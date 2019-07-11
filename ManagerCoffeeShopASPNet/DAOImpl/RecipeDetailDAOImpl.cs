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
            try
            {
                int id = (from recipeDetail in context.RecipeDetails
                          orderby recipeDetail.RecipeDetailID descending
                          select recipeDetail.RecipeDetailID).FirstOrDefault();
                return id;
            }
            catch(Exception ex)
            {
                return 0;
            }
            
        }
        public IEnumerable<RecipeDetail> GetAllRecipeDetailByRecipeID(int RecipeID)
        {
            try
            {
                IEnumerable<RecipeDetail> recipeDetails = from recipedetail in this.context.RecipeDetails
                                                          where recipedetail.RecID == RecipeID
                                                          orderby recipedetail.Step ascending
                                                          select recipedetail;
                int num = recipeDetails.ToList<RecipeDetail>().Count;
                return recipeDetails;
            }
            catch
            {
                return null;
            }
        }
        public RecipeDetail GetAllRecipeDetailByrecipeID(int RecipeID)
        {
            RecipeDetail recipeDetails = (from recipedetail in this.context.RecipeDetails
                                          where recipedetail.RecID == RecipeID
                                          orderby recipedetail.Step ascending
                                          select recipedetail).SingleOrDefault();
            return recipeDetails;
        }
        public RecipeDetail GetRecipeDetailByRecipeDetailID(int RecipeDetailID)
        {
            RecipeDetail recipeDetail = (from RD in context.RecipeDetails
                                         where RD.RecipeDetailID == RecipeDetailID
                                         select RD).SingleOrDefault();
            return recipeDetail;
        }
        public bool InsertRecipeDetail(int RecID, int Step, int IngreID, double Amount, string Unit, string Desc)
        {
            try
            {
                int RecipeDetailID = GetLastRecipeDetailID() + 1;
                RecipeDetail recipeDetail = new RecipeDetail();
                recipeDetail.RecID = RecID;
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
        public int CountRecipeDetailByRecipeID(int RecipeID)
        {
            int result = (from recipeDetail in context.RecipeDetails
                          where recipeDetail.RecID == RecipeID
                          select recipeDetail).Count();
            return result;
        }
        public bool EditRecipeDetail(int RecipeDetailID, int Step, int IngreID, float Amount, string Unit, string Desc)
        {
            try
            {
                RecipeDetail recipedetail = this.context.RecipeDetails.Single(o => o.RecipeDetailID == RecipeDetailID);
                recipedetail.Step = Step;
                recipedetail.IngreID = IngreID;
                recipedetail.Amount = Amount;
                recipedetail.Unit = Unit;
                recipedetail.Desc = Desc;
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Edit RecipeDetail: " + e.Message);
            }
        }
        public bool DeleteRecipeDetail(int RecipeDetailID)
        {
            try
            {
                RecipeDetail recipedetail = this.context.RecipeDetails.Single(o => o.RecipeDetailID == RecipeDetailID);
                context.RecipeDetails.DeleteOnSubmit(recipedetail);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error delete recipeDetail: " + e.Message);
            }
        }
    }
}