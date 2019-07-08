using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface OrderDAO
    {
        int GetLastID();
        bool InsertOrder(int PosID, DateTime OrderDateTime, DateTime PaidDateTime,
            double TotalAmount, string Currency, string Desc, string Status);
        bool InsertOrderWithoutPosID(DateTime OrderDateTime, DateTime PaidDateTime,
           double TotalAmount, string Currency, string Desc, string Status);
        IEnumerable<Order> GetAllOrderByStatus(string Status);
        Order GetOrderByStatus(string Status);
        IEnumerable<Order> GetAllOrderByDescAndStatus(string Desc, string Status);
        bool UpdateOrderStatus(int OrderID, string Status);
        IEnumerable<Order> GetAllOrderPendingOrReady();
        IEnumerable<Order> GetAllOrder();
        IEnumerable<Order> GetOrderByOrderID(int OrderID);
    }
}
