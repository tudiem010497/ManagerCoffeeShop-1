using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface PayrollDAO
    {
        Payroll GetPayrollByEmployeeID(int EmployeeID);
        bool InsertPayroll(int EmployeeID, string EmployeeName, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc, DateTime AddedOn);
        Payroll GetPayrollByAddedOn(DateTime AddedOn);
        IEnumerable<Payroll> GetAllAddedOnOfPayroll();
        IEnumerable<Payroll> GetParyollByEmployeeIDAndAddedOn(int EmployeeID, DateTime AddedOn);
    }
}
