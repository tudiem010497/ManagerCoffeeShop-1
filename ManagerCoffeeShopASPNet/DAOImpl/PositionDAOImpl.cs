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
        public IEnumerable<Position> GetAllPositionByStatus(string Status)
        {
            try
            {
                IEnumerable<Position> pos = from position in context.Positions
                                            where position.Status == Status
                                            select position;
                return pos;
            }
            catch
            {
                return null;
            }
        }
        public bool UpdateStatusPostion(int PosID,string Status)
        {
            try
            {
                Position pos = (from position in context.Positions
                                where position.PosID == PosID
                                select position).SingleOrDefault();
                pos.Status = Status;
                this.context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}