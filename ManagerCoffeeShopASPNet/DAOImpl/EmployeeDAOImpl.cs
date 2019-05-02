using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class EmployeeDAOImpl : EmployeeDAO
    {
        private CoffeeShopDBDataContext context;
        public EmployeeDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees.ToList();
        }
        public int GetLastEmployeeID()
        {
            try
            {
                int id = (from em in context.Employees orderby em.EmployeeID descending select em.EmployeeID).FirstOrDefault();
                return id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int GetLastUserID()
        {
            try
            {
                int id = (from em in context.Accounts orderby em.UserID descending select em.UserID).FirstOrDefault();
                return id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int GetLastCSID()
        {
            try
            {
                int id = (from em in context.CoffeeShops orderby em.CSID descending select em.CSID).FirstOrDefault();
                return id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public Employee GetEmployeeAccountByUserID(int UserID)
        {
            Employee em = (from employee in context.Employees
                           where employee.UserID == UserID
                           select employee).SingleOrDefault();
            return em;
        }
        public IEnumerable<Employee> GetEmployeeByEmployeeID(int EmployeeID)
        {
            IEnumerable<Employee> em = (from employee in context.Employees
                                        where employee.EmployeeID == EmployeeID
                                        select employee)/*.SingleOrDefault()*/;
            return em;
        }
        public Employee GetEmployeeByID(int EmployeeID)
        {
            Employee em = (from employee in context.Employees
                           where employee.EmployeeID == EmployeeID
                           select employee).SingleOrDefault();
            return em;
        }
        public Employee GetEmployeeByEmployeeIDToDelete(int EmployeeID)
        {
            var em = (from employee in context.Employees
                      where employee.EmployeeID == EmployeeID && employee.Status == "Lay_off"
                      select employee).SingleOrDefault();
            return em;
        }
        public bool InsertEmployee(string Name, string Email, string Address, string Phone, DateTime DOB, string Gender, string IndentityNum, string Status)
        {
            try
            {
                //int EmployeeID = GetLastEmployeeID() + 1;
                //int UserID = GetLastUserID() + 1;
                int CSID = GetLastCSID();
                Employee em = new Employee();
                //em.UserID = UserID;
                em.CSID = CSID;
                em.Name = Name;
                em.Email = Email;
                em.Address = Address;
                em.Phone = Phone;
                em.DOB = DOB;
                em.Gender = Gender;
                em.IndentityNum = IndentityNum;
                em.Status = Status;
                context.Employees.InsertOnSubmit(em);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error insert employee " + e.Message);
            }
        }
        public bool DeleteEmployee(int EmployeeID)
        {
            try
            {
                Employee em = GetEmployeeByID(EmployeeID);
                if (em.Status == "Lay_off")
                {
                    context.Employees.DeleteOnSubmit(em);
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception("Error delete employee " + e.Message);
            }
        }
        public bool EditEmployee(Employee employee)
        {
            try
            {
                Employee em = context.Employees.FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
                em.EmployeeID = employee.EmployeeID;
                //em.UserID = employee.UserID;
                em.CSID = employee.CSID;
                em.Name = employee.Name;
                em.Email = employee.Email;
                em.Address = employee.Address;
                em.Phone = employee.Phone;
                em.DOB = employee.DOB;
                em.Gender = employee.Gender;
                em.IndentityNum = employee.IndentityNum;
                em.Status = employee.Status;
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error edit information of employee" + e.Message);
            }
        }
        public bool EditEmployeeMissingUserID(string Email, int UserID)
        {
            try
            {
                Employee em = context.Employees.FirstOrDefault(e => e.Email == Email);
                em.UserID = UserID;
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error edit information of employee" + e.Message);
            }
        }
    }
}