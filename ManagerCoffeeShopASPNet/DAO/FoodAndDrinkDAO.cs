using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface FoodAndDrinkDAO
    {
        int GetLastID();
        IEnumerable<FoodAndDrink> GetAllFoodAndDrink();
        FoodAndDrink GetFoodAndDrinkByID(int id);
        bool InsertFoodAndDrink(string Name, string Desc,
            string ImagePath, string Size, string Type, double UnitPrice, string Currency);
        bool DeleteFoodAndDrinkByFDID(int FDID);
        bool EditFoodAndDrink(FoodAndDrink foodAndDrink);
    }
}
