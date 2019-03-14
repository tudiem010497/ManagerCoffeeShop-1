using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class PositionDAOImpl: PositionDAO
    {
        CoffeeShopDBDataContext context;
        public PositionDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public IEnumerable<Position> GetAllPosition()
        {
            return this.context.Positions.ToList();
        }
    }
}