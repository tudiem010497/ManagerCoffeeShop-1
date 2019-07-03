using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class BasicSalaryDAOImpl:BasicSalaryDAO
    {
        private CoffeeShopDBDataContext context;
        public BasicSalaryDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public BasicSalary GetBasicSalaryByEmployeeID(int EmployeeID)
        {
            try
            {
                BasicSalary bs = (from basicSalary in context.BasicSalaries
                                  where basicSalary.EmployeeID == EmployeeID
                                  select basicSalary).SingleOrDefault();
                return bs;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool InsertBasicSalary(int EmployeeID, int SalaryID)
        {
            try
            {
                BasicSalary basicSalary = new BasicSalary();
                basicSalary.EmployeeID = EmployeeID;
                basicSalary.SalaryID = SalaryID;
                this.context.BasicSalaries.InsertOnSubmit(basicSalary);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Insert BasicSalary: " + e.Message);
            }
        }
        public bool UpdateBasicSalary(int EmployeeID, int SalaryID)
        {
            try
            {
                BasicSalary basicSalary = context.BasicSalaries.FirstOrDefault(bs => bs.EmployeeID == EmployeeID);
                basicSalary.SalaryID = SalaryID;
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Update BasicSalary: " + e.Message);
            }
        }
        public bool DeleteBasicSalary(int EmployeeID)
        {
            try
            {
                BasicSalary basicSalary = context.BasicSalaries.FirstOrDefault(bs => bs.EmployeeID == EmployeeID);
                context.BasicSalaries.DeleteOnSubmit(basicSalary);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error Delete BasicSalary: " + e.Message);
            }
        }
        public bool CheckBasicSalaryOfEmployee(int EmployeeID)
        {
            BasicSalary bs = (from basicSalary in context.BasicSalaries
                              where basicSalary.EmployeeID == EmployeeID
                              select basicSalary).SingleOrDefault();
            return true;
        }
    }
}