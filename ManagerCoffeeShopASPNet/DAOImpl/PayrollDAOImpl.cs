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
        public IEnumerable<Payroll> GetParyollByEmployeeIDAndAddedOn(int Employee, DateTime AddedOn)
        {
            IEnumerable<Payroll> p = from payroll in context.Payrolls
                                      where payroll.EmployeeID == Employee && payroll.AddedOn == AddedOn
                                      select payroll;
            return p;
        }
        public bool InsertPayroll(int EmployeeID, string EmployeeName, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc, DateTime AddedOn)
        {
            try
            {
                Payroll payroll = new Payroll();
                payroll.EmployeeID = EmployeeID;
                payroll.EmployeeName = EmployeeName;
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
    }
}