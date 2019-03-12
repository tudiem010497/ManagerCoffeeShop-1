using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface FoodAndDrinkDAO
    {
        IEnumerable<FoodAndDrink> GetAllFoodAndDrink();
        FoodAndDrink GetFoodAndDrinkByID(int id);
    }
}
