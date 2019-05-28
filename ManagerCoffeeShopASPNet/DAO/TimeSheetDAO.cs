using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface TimeSheetDAO
    {
        TimeSheet GetTimeSheetByEmployeeID(int EmployeeID);
        bool InsertTimeSheet(int EmployeeID, int WorkDay, int Total, string Currency);
    }
}
