using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class CoffeeLandScapeDAOImpl : CoffeeLandScapeDAO
    {
        private CoffeeShopDBDataContext context;
        public CoffeeLandScapeDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public int GetLastCoffeeLandScapeID()
        {
            try
            {
                int id = (from coffeeLandScape in context.CoffeeLandScapes orderby coffeeLandScape.CLSID descending select coffeeLandScape.CLSID).FirstOrDefault();
                return id;
            }
            catch(Exception ex)
            {
                return 0;
                throw new Exception("Error GetLastCoffeeLandScapeID : " + ex.Message);
            }
        }
        public bool InsertCoffeeLandScape(int CSID, string FloorID, double MapRatio,double width, double height)
        {
            try
            {
                CoffeeLandScape coffeeLandScape = new CoffeeLandScape();
                coffeeLandScape.CSID = CSID;
                coffeeLandScape.FloorID = FloorID;
                coffeeLandScape.MapRatio = MapRatio;
                coffeeLandScape.Width = width;
                coffeeLandScape.Height = height;
                this.context.CoffeeLandScapes.InsertOnSubmit(coffeeLandScape);
                this.context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error InsertCoffeeLandScape" + ex.Message);
            }
        }
        public IEnumerable<CoffeeLandScape> GetAllCoffeeLandScape()
        {
            try
            {
                IEnumerable<CoffeeLandScape> coffeelandscapes = from coffeelandscape
                                                               in context.CoffeeLandScapes
                                                               select coffeelandscape;
                return coffeelandscapes;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public CoffeeLandScape GetCoffeeLandScapeByID(int ID)
        {
            try
            {
                CoffeeLandScape result = (from coffeelandscape
                                         in context.CoffeeLandScapes
                                          where coffeelandscape.CLSID == ID
                                          select coffeelandscape).SingleOrDefault();
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public bool UpdateCoffeeLandScape(int CLSID, float width, float height, float ratio, int CSID, string FloorID)
        {
            try
            {
                CoffeeLandScape landscape = GetCoffeeLandScapeByID(CLSID);
                landscape.Width = width;
                landscape.Height = height;
                landscape.MapRatio = ratio;
                landscape.CSID = CSID;
                landscape.FloorID = FloorID;
                this.context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool DeleteCoffeeLandScape(int CLSID)
        {
            try
            {
                CoffeeLandScape cls = context.CoffeeLandScapes.Single(m => m.CLSID == CLSID);
                context.CoffeeLandScapes.DeleteOnSubmit(cls);
                context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Error delete" + e.Message);
            }
        }
    }
}