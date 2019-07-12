using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class IngredientDAOImpl: IngredientDAO
    {
        CoffeeShopDBDataContext context;
        public IngredientDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastIngredientID()
        {
            try
            {
                int ID = (from ingredient in context.Ingredients
                          select ingredient.IngreID).LastOrDefault();
                return ID;
            }
            catch(Exception ex)
            {
                return 0;
            }
            
        }
        public bool UpdateIngredient(Ingredient ingredient)
        {
            try
            {
                Ingredient ingre = (from ingredients in context.Ingredients
                                    where ingredients.IngreID == ingredient.IngreID
                                    select ingredients).SingleOrDefault();
                ingre.SupplierID = ingredient.SupplierID;
                ingre.Name = ingredient.Name;
                ingre.Amount = ingredient.Amount;
                ingre.Unit = ingredient.Unit;
                ingre.UnitPrice = ingredient.UnitPrice;
                ingre.Currency = ingredient.Currency;
                ingre.Amount = ingredient.Amount;
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error EditIngredient : " + ex.Message);

            }
        }
        public IEnumerable<Ingredient> GetAllIngredient()
        {
            return context.Ingredients.ToList();
        }
        public Ingredient GetIngredientByIngreID(int IngreID)
        {
            Ingredient ingre = (from ingredient in context.Ingredients
                                where ingredient.IngreID == IngreID
                                select ingredient).SingleOrDefault();
            return ingre;
        }
        public bool EditIngredient(int IngreID, int SupplierID, string Name, 
            double Amount, double AmountMin, string Unit, double UnitPrice, string Currency)
        {
            try
            {
                Ingredient ingre = (from ingredient in context.Ingredients
                                    where ingredient.IngreID == IngreID
                                    select ingredient).SingleOrDefault();
                ingre.SupplierID = SupplierID;
                ingre.Name = Name;
                ingre.Amount = Amount;
                ingre.AmountMin = AmountMin;
                ingre.Unit = Unit;
                ingre.UnitPrice = UnitPrice;
                ingre.Currency = Currency;
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
                throw new Exception("Error EditIngredient : " + ex.Message);
                
            }
        }
        public bool InsertIngredient(int SupplierID, string Name,
            double Amount, double AmountMin, string Unit, double UnitPrice, string Currency)
        {
            try
            {
                int IngreID = GetLastIngredientID() + 1;
                Ingredient ingre = new Ingredient();
                ingre.SupplierID = SupplierID;
                ingre.Name = Name;
                ingre.Amount = Amount;
                ingre.AmountMin = AmountMin;
                ingre.Unit = Unit;
                ingre.UnitPrice = UnitPrice;
                ingre.Currency = Currency;
                context.Ingredients.InsertOnSubmit(ingre);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
                throw new Exception("Error InsertIngredient : " + ex.Message);
            }
        }
        public bool UpdateAmountIngredient(int IngreID, double Amount)
        {
            try
            {
                Ingredient ingre = this.context.Ingredients.Single(o => o.IngreID == IngreID);
                ingre.Amount = Amount;
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Update Amount: " + e.Message);
            }
        }
        public IEnumerable<Ingredient> GetAllIngredientEffete()
        {
            try
            {
                IEnumerable<Ingredient> ingre = from ingres in context.Ingredients
                                                where ingres.Amount <= ingres.AmountMin
                                                select ingres;
                return ingre;
            }
            catch
            {
                return null;
            }
        }
    }
}