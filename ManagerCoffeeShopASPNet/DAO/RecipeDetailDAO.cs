using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface RecipeDetailDAO
    {
        IEnumerable<RecipeDetail> GetAllRecipeDetailByRecipeID(int RecipeID);
        bool InsertRecipeDetail(int RecID, int Step, int IngreID, double Amount, string Unit, string Desc);
        int CountRecipeDetailByRecipeID(int RecipeID);
    }
}
