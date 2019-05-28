using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class PayrollDAOImpl:PayrollDAO
    {
        private CoffeeShopDBDataContext context;
        public PayrollDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public bool InsertPayroll(int EmployeeID, string EmployeeName, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc)
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
                context.Payrolls.InsertOnSubmit(payroll);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Insert Payroll: " + e.Message);
            }
        }
    }
}