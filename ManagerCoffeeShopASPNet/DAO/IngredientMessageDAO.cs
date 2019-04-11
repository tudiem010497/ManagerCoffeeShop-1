using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface IngredientMessageDAO
    {
        bool InsertIngredientMessage(int IngreID, double Amount, string Unit, string SendMessage);
        int GetLastIngreMessageID();
    }
}
