using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class SalaryDAOImpl:SalaryDAO
    {
        private CoffeeShopDBDataContext context;
        public SalaryDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public Salary GetSalaryByDesc(string Desc)
        {
            try
            {
                Salary s = (from salary in context.Salaries
                            where salary.Desc == Desc
                            select salary).SingleOrDefault();
                return s;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public Salary GetSalaryBySalaryID(int SalaryID)
        {
            try
            {
                Salary s = (from salary in context.Salaries
                            where salary.SalaryID == SalaryID
                            select salary).SingleOrDefault();
                return s;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}