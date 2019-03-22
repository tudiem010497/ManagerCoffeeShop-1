using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Nhân viên phục vụ giúp khách đặt món
        /// </summary>
        /// <returns></returns>
        [Route("ServiceEmployee")]
        [HttpGet]
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

        /// <summary>
        /// Lấy thông tin đồ uống theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetFoodAndDrinkByID")]
        public ActionResult GetFoodAndDrinkByID(int id)
        {
            FoodAndDrink fd = info.GetFoodAndDrinkByID(id);
            var a = fd.GetType().ToString();
            return Json(new { FDID = fd.FDID, Name = fd.Name, UnitPrice = fd.UnitPrice }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gửi thông tin đồ uống lưu trữ xuống database
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendOrderToBatender")]
        public ActionResult SendOrderToBatender(string json)
        {
            string message = "Gửi quầy pha chế thành công";
            OrderModel test = JsonConvert.DeserializeObject<OrderModel>(json);
            string Desc = test.Desc;
            int PosID = test.PosID;
            double TotalAmount = 0;
            List<OrderItemModel> OrderItemModel = test.OrderItemModel;
            foreach (var item in OrderItemModel)
            {
                TotalAmount = TotalAmount + info.GetFoodAndDrinkByID(item.FoodAndDrinkID).UnitPrice * item.Quantity;
            }
            info.InsertOrder(PosID, DateTime.Now, DateTime.Now, TotalAmount, "VND", Desc, "Pending");
            int OrderID = info.GetLastOrderIDID();
            foreach(var item in OrderItemModel)
            {
                info.InsertOrderItem(OrderID, item.FoodAndDrinkID, item.Quantity, item.Desc, "Pending");
            }
            return Json(new { PosID = PosID }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xem đồ uống đã hoàn thành để phục vụ khách
        /// </summary>
        /// <returns></returns>
        [Route("GetListOrderService")]
        public ActionResult GetListOrderService()
        {
            string status = "Ready";
            IEnumerable<Order> orders = info.GetAllOrderByStatus(status);
            ViewData["orders"] = orders;
            foreach(Order order in orders)
            {
                ViewData[order.OrderID.ToString()] = info.GetAllOrderItemByOrderID(order.OrderID);
            }
            return View();
        }
        public ActionResult GetOrderItemByOrderID(int OrderID)
        {
            return View(info.GetAllOrderItemByOrderID(OrderID));
        }
    }
}