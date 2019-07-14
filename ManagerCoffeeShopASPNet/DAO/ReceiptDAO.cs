using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface ReceiptDAO
    {
        IEnumerable<Receipt> GetAllReceipt();
        IEnumerable<Receipt> GetReceiptWaitToConfirm();
        IEnumerable<Receipt> GetReceiptByReceiptID(int ReceiptID);
        Receipt GetReceiptByID(int ReceiptID);
        int GetLastReceiptID();
        bool InsertReceipt(DateTime Date, double TotalAmount, string Currency, string Status);
        bool UpdateReceipt(int ReceiptID, string Status);
    }
}
