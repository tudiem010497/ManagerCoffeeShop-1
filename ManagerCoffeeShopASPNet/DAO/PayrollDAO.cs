using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface PayrollDAO
    {
        Payroll GetPayrollByPayrollID(int PayrollID);
        Payroll GetPayrollByEmployeeID(int EmployeeID);
        Payroll GetPayrollByEmployeeIDAndAddedOnNo(int EmployeeID, DateTime AddedOn);
        bool InsertPayroll(int EmployeeID, string EmployeeName,int BasicSalary, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc, DateTime AddedOn);
        Payroll GetPayrollByAddedOn(DateTime AddedOn);
        IEnumerable<Payroll> GetAllAddedOnOfPayroll();
        IEnumerable<Payroll> GetParyollByEmployeeIDAndAddedOn(int EmployeeName, DateTime AddedOn);
        bool EditPayroll(int PayrollID, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc);
    }
}
