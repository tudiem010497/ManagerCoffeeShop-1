using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class TimeSheetDetailDAOImpl:TimeSheetDetailDAO
    {
        private CoffeeShopDBDataContext context;

        public TimeSheetDetailDAOImpl()
        {
            context = new CoffeeShopDBDataContext();
        }
        public bool InsertTimeSheetDetail(int TimeSheetID, int Bonus, int Penalty, string Currency, string Desc)
        {
            try
            {
                TimeSheetDetail tsDetail = new TimeSheetDetail();
                tsDetail.TimeSheetID = TimeSheetID;
                tsDetail.Bonus = Bonus;
                tsDetail.Penalty = Penalty;
                tsDetail.Desc = Desc;
                tsDetail.Currency = Currency;
                context.TimeSheetDetails.InsertOnSubmit(tsDetail);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error Insert TimeSheetDetail: " + e.Message);
            }
        }
    }
}