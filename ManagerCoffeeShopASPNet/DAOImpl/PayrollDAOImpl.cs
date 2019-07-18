using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class PayrollDAOImpl : PayrollDAO
    {
        private CoffeeShopDBDataContext context;
        public PayrollDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public Payroll GetPayrollByEmployeeID(int EmployeeID)
        {
            try
            {
                Payroll p = (from payroll in context.Payrolls
                             where payroll.EmployeeID == EmployeeID
                             select payroll).SingleOrDefault();
                return p;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Payroll GetPayrollByPayrollID(int PayrollID)
        {
            try
            {
                Payroll p = (from payroll in context.Payrolls
                             where payroll.PayrollID == PayrollID
                             select payroll).SingleOrDefault();
                return p;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Payroll GetPayrollByAddedOn(DateTime AddedOn)
        {
            Payroll p = (from payroll in context.Payrolls
                         where payroll.AddedOn == AddedOn
                         select payroll).SingleOrDefault();
            return p;
        }
        public IEnumerable<Payroll> GetAllAddedOnOfPayroll()
        {
            IEnumerable<Payroll> p = from payroll in context.Payrolls
                                     select payroll;
            return p;
        }
        public IEnumerable<Payroll> GetAllAddedOnOfPayrollByEmID(int EmID)
        {
            try
            {
                IEnumerable<Payroll> p = from payroll in context.Payrolls
                                         where payroll.EmployeeID == EmID
                                         select payroll;
                return p;
            }
            catch
            {
                return null;
            }
            
        }
        public Payroll GetPayrollByEmployeeIDAndAddedOnNo(int EmployeeID, DateTime AddedOn)
        {
            try
            {
                Payroll p = (from payroll in context.Payrolls
                             where payroll.EmployeeID == EmployeeID && payroll.AddedOn == AddedOn
                             select payroll).LastOrDefault();
                return p;
            }
            catch 
            {
                return null;
            }
        }
        public IEnumerable<Payroll> GetParyollByEmployeeIDAndAddedOn(int Employee, DateTime AddedOn)
        {
            IEnumerable<Payroll> p = from payroll in context.Payrolls
                                     where payroll.EmployeeID == Employee && payroll.AddedOn == AddedOn
                                     select payroll;
            return p;
        }
        public bool InsertPayroll(int EmployeeID, string EmployeeName, int BasicSalary, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc, DateTime AddedOn)
        {
            try
            {
                Payroll payroll = new Payroll();
                payroll.EmployeeID = EmployeeID;
                payroll.EmployeeName = EmployeeName;
                payroll.BasicSalary = BasicSalary;
                payroll.WorkDay = WorkDay;
                payroll.Bonus = Bonus;
                payroll.Penalty = Penalty;
                payroll.Total = Total;
                payroll.Currency = Currency;
                payroll.Desc = Desc;
                payroll.AddedOn = AddedOn;
                context.Payrolls.InsertOnSubmit(payroll);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error Insert Payroll: " + e.Message);
            }
        }
        public bool EditPayroll(int PayrollID, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc)
        {
            try
            {
                Payroll payroll = context.Payrolls.FirstOrDefault(p => p.PayrollID == PayrollID);
                payroll.WorkDay = WorkDay;
                payroll.Bonus = Bonus;
                payroll.Penalty = Penalty;
                payroll.Total = Total;
                payroll.Currency = Currency;
                payroll.Desc = Desc;
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error Edit Payroll: " + e.Message);
            }
        }
    }
}