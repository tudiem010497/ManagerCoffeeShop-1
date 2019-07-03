using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class TimeSheetDAOImpl : TimeSheetDAO
    {
        private CoffeeShopDBDataContext context;
        public TimeSheetDAOImpl()
        {
            context = new CoffeeShopDBDataContext();
        }
        public TimeSheet GetTimeSheetByEmployeeID(int EmployeeID)
        {
            TimeSheet ts = (from timesheet in context.TimeSheets
                            where timesheet.EmployeeID == EmployeeID
                            select timesheet).SingleOrDefault();
            return ts;
        }
        public bool InsertTimeSheet(int EmployeeID, int WorkDay, int Total, string Currency)
        {
            try
            {
                TimeSheet timeSheet = new TimeSheet();
                timeSheet.EmployeeID = EmployeeID;
                timeSheet.TotalDay = WorkDay;
                timeSheet.TotalAmount = Total;
                timeSheet.Currency = Currency;
                context.TimeSheets.InsertOnSubmit(timeSheet);
                context.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error insert timeSheet: " + e.Message);
            }
        }
        public bool CheckTimeSheetOfEmployee(int EmployeeID)
        {
            try
            {
                TimeSheet bs = (from ts in context.TimeSheets
                                where ts.EmployeeID == EmployeeID
                                select ts).Single();
                if (EmployeeID == bs.EmployeeID)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}