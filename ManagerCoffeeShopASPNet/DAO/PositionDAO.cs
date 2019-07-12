using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffeeShopASPNet.DAO
{
    interface PositionDAO
    {
        IEnumerable<Position> GetAllPosition();
        IEnumerable<Position> GetAllPositionByStatus(string Status);
        bool UpdateStatusPostion(int PosID, string Status);
    }
}
