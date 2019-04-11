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
        public IEnumerable<Ingredient> GetAllIngredient()
        {
            return context.Ingredients.ToList();
        }
    }
}