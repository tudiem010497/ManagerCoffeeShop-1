using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface ReceiptDetailDAO
    {
        IEnumerable<ReceiptDetail> GetReceiptDetailByReceiptID(int ReceiptID);
        bool InsertReceiptDetail(int ReceiptID, int IngreID, double Amount, string Unit, double UnitPrice, string Currency, string Status);
    }
}
