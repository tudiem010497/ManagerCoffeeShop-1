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
        ReceiptDetail GetReceiptDetailByReceiptDetailID(int ReceiptDetailID);
        bool InsertReceiptDetail(int ReceiptID, int IngreID, int GiftID, double Amount, string Unit, double UnitPrice, string Currency, string Status);
        bool InsertReceiptDetailMissIngreID(int ReceiptID, int GiftID, double Amount, double UnitPrice, string ReferenceDesc, string Currency, string Status);
        bool InsertReceiptDetailMissGiftID(int ReceiptID, int IngreID, double Amount, string Unit, double UnitPrice, string ReferenceDesc, string Currency, string Status);
        bool UpdateReceiptDetail(int ReceiptDetailID, string Status);
    }
}
