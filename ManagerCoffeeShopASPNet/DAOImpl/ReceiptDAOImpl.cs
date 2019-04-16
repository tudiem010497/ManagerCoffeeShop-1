using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class ReceiptDAOImpl:ReceiptDAO
    {
        private CoffeeShopDBDataContext context;
        public ReceiptDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Receipt> GetAllReceipt()
        {
            try
            {
                return context.Receipts.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}