using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface TimeSheetDetailDAO
    {
        bool InsertTimeSheetDetail(int TimeSheetID, int Bonus, int Penalty, string Currency, string Desc);
    }
}
