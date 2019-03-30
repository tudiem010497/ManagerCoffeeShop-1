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
        public int GetLastID()
        {
            int id = (from fd in context.FoodAndDrinks
                      orderby fd.FDID descending
                      select fd.FDID).FirstOrDefault();
            return id;
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
        public bool InsertFoodAndDrink(string Name, string Desc,
            string ImagePath, string Size, string Type, double UnitPrice, string Currency)
        {
            int FDID = GetLastID() + 1;
            try
            {
                FoodAndDrink fd = new FoodAndDrink();
                fd.Name = Name;
                fd.Desc = Desc;
                fd.ImagePath = ImagePath;
                fd.Size = Size;
                fd.Type = Type;
                fd.UnitPrice = UnitPrice;
                fd.Currency = Currency;
                context.FoodAndDrinks.InsertOnSubmit(fd);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Insert FoodAndDrink " + ex.Message);
            }
        }
        public bool DeleteFoodAndDrinkByFDID(int FDID)
        {
            try
            {
                FoodAndDrink fd = GetFoodAndDrinkByID(FDID);
                context.FoodAndDrinks.DeleteOnSubmit(fd);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error DeleteFoodAndDrinkByFDID" + ex.Message);
            }
        }
        public bool EditFoodAndDrink(FoodAndDrink foodAndDrink)
        {
            try
            {
                FoodAndDrink fd = context.FoodAndDrinks.FirstOrDefault(f => f.FDID == foodAndDrink.FDID);
                fd.Name = foodAndDrink.Name;
                fd.Desc = foodAndDrink.Desc;
                fd.ImagePath = foodAndDrink.ImagePath;
                fd.Size = foodAndDrink.Size;
                fd.Type = foodAndDrink.Type;
                fd.UnitPrice = foodAndDrink.UnitPrice;
                fd.Currency = foodAndDrink.Currency;
                context.SubmitChanges();
               
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error EditFoodAndDrink" + ex.Message);
            }
        }
    }
}