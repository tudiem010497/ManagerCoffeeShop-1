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
            double Amount, string Unit, double UnitPrice, string Currency)
        {
            try
            {
                Ingredient ingre = (from ingredient in context.Ingredients
                                    where ingredient.IngreID == IngreID
                                    select ingredient).SingleOrDefault();
                ingre.SupplierID = SupplierID;
                ingre.Name = Name;
                ingre.Amount = Amount;
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
            double Amount, string Unit, double UnitPrice, string Currency)
        {
            try
            {
                int IngreID = GetLastIngredientID() + 1;
                Ingredient ingre = new Ingredient();
                ingre.SupplierID = SupplierID;
                ingre.Name = Name;
                ingre.Amount = Amount;
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
    }
}