using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface BasicSalaryDAO
    {
        BasicSalary GetBasicSalaryByEmployeeID(int EmployeeID);
        bool InsertBasicSalary(int EmployeeID, int SalaryID);
        bool UpdateBasicSalary(int EmployeeID, int SalaryID);
    }
}
