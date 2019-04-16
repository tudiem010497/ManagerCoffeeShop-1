using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface EmployeeDAO
    {
        IEnumerable<Employee> GetAllEmployee();
        int GetLastEmployeeID();
        int GetLastUserID();
        int GetLastCSID();
        Employee GetEmployeeAccountByUserID(int UserID);
        Employee GetEmployeeByEmployeeID(int EmployeeID);
        //int UserID, int CSID,
        bool InsertEmployee( string Name, string Email, string Address, string Phone, DateTime DOB, string Gender, string IndentityNum, string Status);
        bool EditEmployee(Employee employee);
        bool EditEmployeeMissingUserID(string Email, int UserID);
        bool DeleteEmployee(int EmployeeID);
    }
}
