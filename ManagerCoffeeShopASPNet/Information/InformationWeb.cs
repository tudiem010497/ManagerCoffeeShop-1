using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.Information
{
    public class InformationWeb
    {
        private EmployeeDAO _employeeDAO;
        private AccountDAO _accountDAO;
        private CoffeeShopDAO _coffeeShopDAO;
        public InformationWeb()
        {
            this._employeeDAO = (EmployeeDAO)new EmployeeDAOImpl();
            this._accountDAO = (AccountDAO)new AccountDAOImpl();
            this._coffeeShopDAO = (CoffeeShopDAO)new CoffeeShopDAOImpl();
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return this._employeeDAO.GetAllEmployee();
        }
        public Employee GetEmployeeByEmployeeID(int EmployeeID)
        {
            return this._employeeDAO.GetEmployeeByEmployeeID(EmployeeID);
        }
        public bool InsertEmployee( string Name, string Email, string Address, string Phone, DateTime DOB, string Gender, string IndentityNum, string Status)
        {
            return this._employeeDAO.InsertEmployee( Name, Email, Address, Phone, DOB, Gender, IndentityNum, Status);
        }
        public bool DeleteEmployee(int EmployeeID)
        {
            return this._employeeDAO.DeleteEmployee(EmployeeID);
        }
        public bool EditEmployee(Employee employee)
        {
            return this._employeeDAO.EditEmployee(employee);
        }
        public Account GetAccountByUserID(int UserID)
        {
            return this._accountDAO.GetAccountByUserID(UserID);
        }
        public Account GetAccountByEmail(string Email)
        {
            return this._accountDAO.GetAccountByEmail(Email);
        }
        public bool InsertAccount(string UserName, string Password, string Email, string AccType, string Position, string Avatar)
        {
            return this._accountDAO.InsertAccount(UserName, Password, Email, AccType, Position, Avatar);
        }
        public bool EditEmployeeMissingUserID(string Email, int UserID)
        {
            return this._employeeDAO.EditEmployeeMissingUserID(Email, UserID);
        }
        public bool DeleteAccount(int UserID)
        {
            return this._accountDAO.DeleteAccount(UserID);
        }
        public IEnumerable<CoffeeShop> GetAllCoffeeShop()
        {
            return this._coffeeShopDAO.GetAllCoffeeShop();
        }
    }
}