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
        int GetLastReceiptID();
        bool InsertReceipt(DateTime Date, double TotalAmount, string Currency, string Status);
    }
}
