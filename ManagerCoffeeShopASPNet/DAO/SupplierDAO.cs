using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface SupplierDAO
    {
        IEnumerable<Supplier> GetAllSupplier();
        Supplier GetSupplierBySupplierID(int SupplierID);
        bool EditSupplier(int SupplierID, string Name, string Address, string Phone);
        bool InsertSupplier(string Name, string Address, string Phone);
    }
}
