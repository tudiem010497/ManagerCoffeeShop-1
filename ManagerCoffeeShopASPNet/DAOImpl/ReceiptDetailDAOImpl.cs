using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class ReceiptDetailDAOImpl:ReceiptDetailDAO
    {
        private CoffeeShopDBDataContext context;
        public ReceiptDetailDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<ReceiptDetail> GetReceiptDetailByReceiptID(int ReceiptID)
        {
            try
            {
                IEnumerable<ReceiptDetail> list = from receiptdetail in context.ReceiptDetails
                                                  where receiptdetail.ReceiptID == ReceiptID
                                                  select receiptdetail;
                return list;                                                  
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}