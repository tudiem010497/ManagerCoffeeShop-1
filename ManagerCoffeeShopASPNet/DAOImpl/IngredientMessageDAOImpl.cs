using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.ManagerSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class IngredientMessageDAOImpl: IngredientMessageDAO
    {
        private CoffeeShopDBDataContext context;
        public IngredientMessageDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastIngreMessageID()
        {
            try
            {
                int id = (from ingreMessage in context.IngredientMessages select ingreMessage).LastOrDefault().IngreMessageID;
                return id;
            }
            catch(Exception ex)
            {
                return 0;
            }
            
        }
        public bool InsertIngredientMessage(int IngreID, int EmployeeID, double Amount, string Unit, string SendMessage)
        {
            try
            {
                int ingreMessageID = GetLastIngreMessageID() + 1;
                IngredientMessage ingreMess = new IngredientMessage();
                ingreMess.EmployeeID = EmployeeID;
                ingreMess.IngreMessageID = ingreMessageID;
                ingreMess.IngreID = IngreID;
                ingreMess.Amount = Amount;
                ingreMess.Unit = Unit;
                ingreMess.SendMessage = SendMessage;
                ingreMess.MessageDateTime = DateTime.Now;
                ingreMess.Status = "WaitingConfirm";
                this.context.IngredientMessages.InsertOnSubmit(ingreMess);
                context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error InsertMessageIngredient: " + ex.Message);
            }
        }
    }
}