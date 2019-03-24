using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface OrderItemDAO
    {
        int GetLastID();
        void InsertOrderItem(int OrderID, int FDID, int Quantity, string Desc, string Status);
        IEnumerable<OrderItem> GetAllOrderItemByOrderID(int id);
        OrderItem GetOrderItemByID(int ID);
        bool UpdateOrderItemStatus(int orderItemID, string status);
        IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndStatus(int OrderID, string Status);
        IEnumerable<OrderItem> GetAllOrderItemByOrderIDAndNeedService(int OrderID);
        IEnumerable<OrderItem> GetAllOrderItemNeedService();
    }
}
