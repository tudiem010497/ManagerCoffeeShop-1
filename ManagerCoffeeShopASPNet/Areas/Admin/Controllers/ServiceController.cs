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
        /// Xem thông tin đồ uống để phục vụ (theo hóa đơn) => hiển thị status = Ready && Pending
        /// </summary>
        /// <returns></returns>
        [Route("GetListOrderServiceGroupByOrder")]
        public ActionResult GetListOrderServiceGroupByOrder()
        {
            IEnumerable<Order> orders = info.GetAllOrderPendingOrReady();
            ViewData["orders"] = orders;
            return View(orders);
        }

        /// <summary>
        /// Chi tiết đơn hàng chỉ hiển thị đơn ở trạng thái pending và cancel, ready
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [Route("DetailOrder")]
        public ActionResult DetailOrder(int OrderID)
        {
            IEnumerable<OrderItem> orderItems = info.GetAllOrderItemByOrderIDAndNeedService(OrderID);
            return View(orderItems);
        }

        /// <summary>
        /// Cập nhật trạng thái đồ uống đã được phục vụ
        /// Nếu đã được pha chế sau khi phục vụ cập nhật => Closed
        /// Nếu là đồ uống quầy pha chế đã hủy sau khi thông báo khách hàng => CancelClosed
        /// </summary>
        /// <param name="OrderItemID"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [Route("UpdatedOrderItemClosed")]
        public ActionResult UpdateOrderItemClosed(int OrderItemID, int OrderID)
        {
            string status1 = "CancelClosed";
            string status2 = "Closed";
            OrderItem orderItem = info.GetOrderItemByOrderItemID(OrderItemID);
            if(orderItem.Status == "Cancel")
            {
                bool result = info.UpdateOrderItemStatus(OrderItemID, status1);
            }
            else
            {
                info.UpdateOrderItemStatus(OrderItemID, status2);
            }
            
            if(info.GetAllOrderItemByOrderIDAndNeedService(OrderID).Count() != 0)
            {
                return RedirectToAction("DetailOrder", "Service", new { OrderID = OrderID });
            }
            else
            {
                info.UpdateOrderStatus(OrderID, status1);
                return RedirectToAction("GetListOrderService", "Service");
            }
        }

        /// <summary>
        /// Cập nhật đã phục vụ 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="confirm"></param>
        /// <returns></returns>
        [Route("UpdatedOrderClosed")]
        public ActionResult UpdatedOrderClosed(int OrderID, string confirm)
        {
            string status = "Closed";
            if(confirm == "true")
            {
                IEnumerable<OrderItem> orderItems = info.GetAllOrderItemByOrderID(OrderID);
                foreach(OrderItem orderItem in orderItems)
                {
                    info.UpdateOrderItemStatus(orderItem.OrderItemID, status);
                }
                info.UpdateOrderStatus(OrderID, status);
            }
            return RedirectToAction("GetListOrderService", "Service");
        }

        /// <summary>
        /// Danh sách đồ uống cần phục vụ theo món
        /// </summary>
        /// <returns></returns>
        [Route("GetListOrderItemNeedServiceGroupByFoodAnDrink")]
        public ActionResult GetListOrderItemNeedServiceGroupByFoodAnDrink()
        {
            List<OrderItemGroupByFoodAndDrink> list = new List<OrderItemGroupByFoodAndDrink>();
            IEnumerable<OrderItem> orderItems = info.GetAllOrderItemNeedService();
            foreach(OrderItem item in orderItems)
            {
                foreach(OrderItemGroupByFoodAndDrink group in list)
                {
                    if (item.FDID == group.FoodAndDrink.FDID)
                    {
                        group.Quantity = item.Quantity + group.Quantity;
                        group.OrderItems.Add(item);
                    }
                    else
                    {
                        OrderItemGroupByFoodAndDrink orderItemGroupByFoodAndDrink = new OrderItemGroupByFoodAndDrink();
                        orderItemGroupByFoodAndDrink.FoodAndDrink = item.FoodAndDrink;
                        orderItemGroupByFoodAndDrink.OrderItems.Add(item);
                        orderItemGroupByFoodAndDrink.Quantity = item.Quantity;
                        list.Add(orderItemGroupByFoodAndDrink);
                    }
                }
            }
            return View(list);
        }
    }
}