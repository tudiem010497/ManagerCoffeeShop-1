using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface OrderItemDAO
    {
        void InsertOrderItem(int OrderID, int FDID, int Quantity, string Desc, string Status);
        IEnumerable<OrderItem> GetAllOrderItemByOrderID(int id);
    }
}
