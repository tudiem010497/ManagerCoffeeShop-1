using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface IngreExchangeDAO
    {
        bool InsertIngreExchange(IngreExchange ingreExchange);
        IngreExchange GetIngreExchangeByIngreExchangeID(int IngreExchangeID);
        IngreExchange GetIngreExchangeByRecipeDetailID(int RecipeDetailID);
        bool EditIngreExchange(IngreExchange exchange);
    }
}
