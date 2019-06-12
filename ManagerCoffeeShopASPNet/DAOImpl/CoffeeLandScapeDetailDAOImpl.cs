using ManagerCoffeeShopASPNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerCoffeeShopASPNet.DAOImpl
{
    public class CoffeeLandScapeDetailDAOImpl: CoffeeLandScapeDetailDAO
    {
        CoffeeShopDBDataContext context;
        public CoffeeLandScapeDetailDAOImpl()
        {
            this.context = new CoffeeShopDBDataContext();
        }
        public bool InsertCoffeeLandScapeDetail(int CLSID, string Href,float x, float y, float width, float height, int Rotate)
        {
            try
            {
                CoffeeLandScapeDetail detail = new CoffeeLandScapeDetail();
                detail.CLSID = CLSID;
                detail.Href = Href;
                detail.x = x;
                detail.y = y;
                detail.Width = width;
                detail.Height = height;
                detail.Rotate = Rotate;
                this.context.CoffeeLandScapeDetails.InsertOnSubmit(detail);
                this.context.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("InsertCoffeeLandScapeDetail : " + ex.Message);
            }
        }
        public IEnumerable<CoffeeLandScapeDetail> GetAllCoffeeLandScapeDetailByCoffeeLandScapeID(int CoffeeLandScapeID)
        {
            try
            {
                IEnumerable<CoffeeLandScapeDetail> details = (from detail 
                                                              in context.CoffeeLandScapeDetails
                                                              where detail.CLSID == CoffeeLandScapeID
                                                              select detail);
                return details;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public bool UpdateCoffeeLandScapeDetail(int CLSDetailID, int CLSID, string Href, float x, float y, float width, float height, int Rotate)
        {
            try
            {
                CoffeeLandScapeDetail detail = (from coffeeLandScapeDetail
                                               in context.CoffeeLandScapeDetails
                                                where coffeeLandScapeDetail.ItemID == CLSDetailID
                                                select coffeeLandScapeDetail).SingleOrDefault();
                detail.CLSID = CLSID;
                detail.Href = Href;
                detail.x = x;
                detail.y = y;
                detail.Width = width;
                detail.Height = height;
                detail.Rotate = Rotate;
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckCoffeeLandScapeDetailIsExistsByID(int ID)
        {
            try
            {
                CoffeeLandScapeDetail detail = (from coffeeLandScapeDetail in context.CoffeeLandScapeDetails
                                                where coffeeLandScapeDetail.ItemID == ID
                                                select coffeeLandScapeDetail).Single();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool DeleteCoffeeLandScapeDetail(int CLSDetailID)
        {
            try
            {
                CoffeeLandScapeDetail detail = this.context.CoffeeLandScapeDetails.Single(o => o.ItemID == CLSDetailID);
                this.context.CoffeeLandScapeDetails.DeleteOnSubmit(detail);
                this.context.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}