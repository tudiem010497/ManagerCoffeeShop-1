using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class IngreExchangeDAOImpl: IngreExchangeDAO
    {
        CoffeeShopDBDataContext context;
        public IngreExchangeDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public bool InsertIngreExchange(IngreExchange ingreExchange)
        {
            try
            {
                this.context.IngreExchanges.InsertOnSubmit(ingreExchange);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public IngreExchange GetIngreExchangeByIngreExchangeID(int IngreExchangeID)
        {
            try
            {
                IngreExchange exchange = (from ex in context.IngreExchanges
                                          where ex.IngreExchangeID == IngreExchangeID
                                          select ex).SingleOrDefault();
                return exchange;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public IngreExchange GetIngreExchangeByRecipeDetailID(int RecipeDetailID)
        {
            try
            {
                IngreExchange exchange = (from ex in context.IngreExchanges
                                          where ex.RecipeDetailID == RecipeDetailID
                                          select ex).SingleOrDefault();
                return exchange;
            }
            catch
            {
                return null;
            }
        }
        public bool EditIngreExchange(IngreExchange exchange)
        {
            try
            {
                IngreExchange ex = (from exc in context.IngreExchanges
                                          where exc.IngreExchangeID == exchange.IngreExchangeID
                                          select exc).SingleOrDefault();
                ex.FDID = exchange.FDID;
                ex.IngreID = exchange.IngreID;
                ex.Quantity = exchange.Quantity;
                ex.RecipeDetailID = exchange.RecipeDetailID;
                ex.Unit = exchange.Unit;
                this.context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}