using ManagerCoffeeShopASPNet.Information;
using ManagerCoffeeShopASPNet.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("service")]
    public class ServiceController : Controller
    {
        private InformationService info = new InformationService();
        // GET: Admin/Service
        public ActionResult Index()
        {
            return View();
        }
        [Route("serviceOrder")]
        public ActionResult ServiceEmployee()
        {

            IEnumerable<FoodAndDrink> fds = info.GetFoodAndDrink();
            IEnumerable<Position> positions = info.GetAllPosition();
            ViewData["positions"] = positions;
            ViewData["fds"] = fds;
            return View();
        }
        [Route("ListFoodAndDrink")]
        public ActionResult ListFoodAndDrink()
        {
            IEnumerable<FoodAndDrink> fds = info.GetFoodAndDrink();
            return Json(fds, JsonRequestBehavior.AllowGet);
        }
        [Route("GetFoodAndDrinkByID")]
        public ActionResult GetFoodAndDrinkByID(int id)
        {
            FoodAndDrink fd = info.GetFoodAndDrinkByID(id);
            return Json(new { FDID = fd.FDID, Name = fd.Name, UnitPrice = fd.UnitPrice }, JsonRequestBehavior.AllowGet);
        }
        //[Route("SendOrderToBatender")]
        //public ActionResult SendOrderToBatender()
        //{

        //}


    }
}