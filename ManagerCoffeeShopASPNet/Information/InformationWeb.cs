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
        private BasicSalaryDAO _basicSalaryDAO;
        private SalaryDAO _salaryDAO;
        public InformationWeb()
        {
            this._employeeDAO = (EmployeeDAO)new EmployeeDAOImpl();
            this._accountDAO = (AccountDAO)new AccountDAOImpl();
            this._coffeeShopDAO = (CoffeeShopDAO)new CoffeeShopDAOImpl();
            this._basicSalaryDAO = (BasicSalaryDAO)new BasicSalaryDAOImpl();
            this._salaryDAO = (SalaryDAO)new SalaryDAOImpl();
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return this._employeeDAO.GetAllEmployee();
        }
        public IEnumerable<Employee> GetEmployeeByEmployeeID(int EmployeeID)
        {
            return this._employeeDAO.GetEmployeeByEmployeeID(EmployeeID);
        }
        public Employee GetEmployeeByID(int EmployeeID)
        {
            return this._employeeDAO.GetEmployeeByID(EmployeeID);
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
        public IEnumerable<Account> GetAccountByID(int UserID)
        {
            return this._accountDAO.GetAccountByID(UserID);
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
        public IEnumerable<CoffeeShop> GetCoffeeShopByCSID(int CSID)
        {
            return this._coffeeShopDAO.GetCoffeeShopByCSID(CSID);
        }
        public bool InsertCoffeeShop(string Name, string Address, string Phone, string LogoImagePath, string TitleAbout, string DescAbout, string TitleContact, string DescContact, string Email)
        {
            return this._coffeeShopDAO.InsertCoffeeShop(Name, Address, Phone, LogoImagePath, TitleAbout, DescAbout, TitleContact, DescContact, Email);
        }
        public bool EditCoffeeShop(CoffeeShop cs)
        {
            return this._coffeeShopDAO.EditCoffeeShop(cs);
        }
        public BasicSalary GetBasicSalaryByEmployeeID(int EmployeeID)
        {
            return this._basicSalaryDAO.GetBasicSalaryByEmployeeID(EmployeeID);
        }
        public bool InsertBasicSalary(int EmployeeID, int SalaryID)
        {
            return this._basicSalaryDAO.InsertBasicSalary(EmployeeID, SalaryID);
        }
        public int GetLastEmployeeID()
        {
            return this._employeeDAO.GetLastEmployeeID();
        }
        public Salary GetSalaryByDesc(string Desc)
        {
            return this._salaryDAO.GetSalaryByDesc(Desc);
        }
        public Salary GetSalaryBySalaryID(int SalaryID)
        {
            return this._salaryDAO.GetSalaryBySalaryID(SalaryID);
        }
        public bool UpdateBasicSalary(int EmployeeID, int SalaryID)
        {
            return this._basicSalaryDAO.UpdateBasicSalary(EmployeeID, SalaryID);
        }
    }
}