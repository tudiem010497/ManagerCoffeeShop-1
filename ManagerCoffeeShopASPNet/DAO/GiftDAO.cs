using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface GiftDAO
    {
        IEnumerable<Gift> GetAllGift();
        int GetGiftID();
        Gift GetGiftByID(int GiftID);
        bool InsertGift(int SupplierID, string Name, float UnitPrice, string Currency, string Desc);
        bool EditGift(Gift gift);
        bool DeleteGift(int GiftID);
    }
}
