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
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
        //[Route("ServiceEmployee")]
        //[HttpGet]
        //public ActionResult ServiceEmployee()
        //{
        //    IEnumerable<FoodAndDrink> fds = info.GetFoodAndDrink();
        //    IEnumerable<Position> positions = info.GetAllPosition();
        //    ViewData["positions"] = positions;
        //    ViewData["fds"] = fds;
        //    return View();
        //}

        [Route("ServiceEmployee")]
        [HttpGet]
        public ActionResult ServiceEmployee(string message)
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
            var a = fd.GetType().ToString();
            return Json(new { FDID = fd.FDID, Name = fd.Name, UnitPrice = fd.UnitPrice }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult SendOrderToBatender(string json)
        //{
        //    string message = "Gửi quầy pha chế thành công";
        //    OrderModel test = JsonConvert.DeserializeObject<OrderModel>(json);
        //    string Desc = test.Desc;
        //    int PosID = test.PosID;
        //    double TotalAmount = 0;
        //    List<OrderItemModel> OrderItemModel = test.OrderItemModel;
        //    foreach (var item in OrderItemModel)
        //    {
        //        TotalAmount = TotalAmount + info.GetFoodAndDrinkByID(item.FoodAndDrinkID).UnitPrice * item.Quantity;
        //    }
        //    info.InsertOrder(PosID, DateTime.Now, DateTime.Now, TotalAmount, "VND", Desc, "Chưa thực hiện");
        //    return RedirectToAction("Index", "Service");
        //}

        [HttpPost]
        [Route("SendOrderToBatender")]
        public ActionResult SendOrderToBatender(string json)
        {
            //string message = "Gửi quầy pha chế thành công";
            //OrderModel test = JsonConvert.DeserializeObject<OrderModel>(json);
            //string Desc = test.Desc;
            //int PosID = test.PosID;
            //double TotalAmount = 0;
            //List<OrderItemModel> OrderItemModel = test.OrderItemModel;
            //foreach (var item in OrderItemModel)
            //{
            //    TotalAmount = TotalAmount + info.GetFoodAndDrinkByID(item.FoodAndDrinkID).UnitPrice * item.Quantity;
            //}
            //info.InsertOrder(PosID, DateTime.Now, DateTime.Now, TotalAmount, "VND", Desc, "Pending");
            bool result = info.InsertOrder(1, DateTime.Now, DateTime.Now, 12.2, "VND", "ABC", "Pending");
            return RedirectToAction("ServiceEmployee", "Service");
        }

    }
}