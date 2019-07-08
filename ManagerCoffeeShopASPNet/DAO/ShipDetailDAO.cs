using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface ShipDetailDAO
    {
        IEnumerable<ShipDetail> GetListShipDelivery();
        //string Status
        IEnumerable<ShipDetail> GetShipDeliveryByStatus();
        IEnumerable<ShipDetail> GetShipDeliveryWaitToConfirm();
        ShipDetail GetShipDetailByOrderID(int OrderID);
        ShipDetail GetShipDeliveryByShipDetailID(int ShipDetailID);
        bool UpdateShipDetailStatus(int OrderID, string Status);
        bool UpdateShipDetailStatusByShipDetailID(int ShipDetailID, string Status);
        bool InsertShipDetail(int ShipID, int OrderID, string CustName, string Address, string Phone, string Status);   
    }
}
