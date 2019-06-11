using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface CoffeeLandScapeDAO
    {
        bool InsertCoffeeLandScape(int CSID, string FloorID, double MapRatio, double width, double height);
        int GetLastCoffeeLandScapeID();
        IEnumerable<CoffeeLandScape> GetAllCoffeeLandScape();
        CoffeeLandScape GetCoffeeLandScapeByID(int ID);
        bool UpdateCoffeeLandScape(int CLSID, float width, float height, float ratio, int CSID, string FloorID);
    }
}
