using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface IngredientDAO
    {
        IEnumerable<Ingredient> GetAllIngredient();
        Ingredient GetIngredientByIngreID(int IngreID);
        bool EditIngredient(int IngreID, int SupplierID, string Name,
            double Amount, double AmountMin, string Unit, double UnitPrice, string Currency);
        bool InsertIngredient(int SupplierID, string Name,
            double Amount, double AmountMin, string Unit, double UnitPrice, string Currency);
        bool UpdateAmountIngredient(int IngreID, double Amount);
        bool UpdateIngredient(Ingredient ingredient);
        IEnumerable<Ingredient> GetAllIngredientEffete();
    }
}
