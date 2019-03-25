using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface RecipeDAO
    {
        Recipe GetRecipeByFoodDrinkID(int FoodAndDrinkID);
    }
}
