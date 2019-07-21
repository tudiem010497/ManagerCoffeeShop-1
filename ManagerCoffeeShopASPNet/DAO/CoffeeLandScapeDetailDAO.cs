using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface CoffeeLandScapeDetailDAO
    {
        int GetLastCoffeeLandScapeDetailID();
        bool InsertCoffeeLandScapeDetail(int CLSID, string Href, float x, float y, float width, float height, int Rotate);
        IEnumerable<CoffeeLandScapeDetail> GetAllCoffeeLandScapeDetailByCoffeeLandScapeID(int CoffeeLandScapeID);
        bool UpdateCoffeeLandScapeDetail(int CLSDetailID, int CLSID, string Href, float x, float y, float width, float height, int Rotate);
        bool CheckCoffeeLandScapeDetailIsExistsByID(int ID);
        bool DeleteCoffeeLandScapeDetail(int CLSDetailID);
    }
}
