//using CrystalDecisions.CrystalReports.Engine;
using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Infrastructure;
using System.IO;
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
        public ActionResult UpdateOrderItemClosed(int OrderItemID, int OrderID, string View)
        {
            string status1 = "CancelClosed";
            string status2 = "Closed";
            OrderItem orderItem = info.GetOrderItemByOrderItemID(OrderItemID);
            // Nếu status là cancel cập nhật status của orderitem là status1 ngược lại là status 2
            if(orderItem.Status == "Cancel")
            {
                bool result = info.UpdateOrderItemStatus(OrderItemID, status1);
            }
            else
            {
                info.UpdateOrderItemStatus(OrderItemID, status2);
            }
            
            // Nếu đang ở view Chi tiết hóa đơn 
            if(View == "DetailOrder")
            {
                // Nếu chi tiết hóa đơn != 0 thì redirect về DetailOrder ngược lại thì redirect về GetListOrderService
                if (info.GetAllOrderItemByOrderIDAndNeedService(OrderID).Count() != 0)
                {
                    return RedirectToAction("DetailOrder", "Service", new { OrderID = OrderID });
                }
                else
                {
                    info.UpdateOrderStatus(OrderID, status1);
                    return RedirectToAction("GetListOrderService", "Service");
                }
            }
            // nếu đang ở view xem thông tin pha chế nhóm theo từng loại món
            else
            {
                return RedirectToAction("GetListOrderItemNeedServiceGroupByFoodAnDrink", "Service");
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
        //[Route("GetListOrderItemNeedServiceGroupByFoodAnDrink")]
        //public ActionResult GetListOrderItemNeedServiceGroupByFoodAnDrink()
        //{
        //    ListOrderItemGroupByFoodAndDrink listGroup = new ListOrderItemGroupByFoodAndDrink();
        //    IEnumerable<OrderItem> orderItems = info.GetAllOrderItemNeedService();
        //    int index;
        //    foreach(OrderItem item in orderItems)
        //    {
        //        index = listGroup.FindIndexFoodAndDrinkInListGroup(item.FDID);
        //        if (index != -1)
        //        {
        //            listGroup.list[index].Quantity = listGroup.list[index].Quantity + item.Quantity;
        //            listGroup.list[index].OrderItems.Add(item);
        //        }
        //        else
        //        {
        //            OrderItemGroupByFoodAndDrink orderItemGroupByFoodAndDrink = new OrderItemGroupByFoodAndDrink();
        //            orderItemGroupByFoodAndDrink.FoodAndDrink = item.FoodAndDrink;
        //            orderItemGroupByFoodAndDrink.OrderItems.Add(item);
        //            orderItemGroupByFoodAndDrink.Quantity = item.Quantity;
        //            listGroup.list.Add(orderItemGroupByFoodAndDrink);
        //        }
        //    }
        //    return View(listGroup.list);
        //}

        [Route("GetAllOrderItem")]
        public ActionResult GetAllOrderItem()
        {
            IEnumerable<OrderItem> orderItems = info.GetAllOrderItem();
            List<OrderItem> list = orderItems.ToList();
            return View(orderItems);
        }
        
        //[Route("PrintReport")]
        //public ActionResult PrintReport()
        //{
        //    //IEnumerable<Order> orders = info.GetAllOrder();
        //    //List<Order> list = orders.ToList();
        //    //foreach(Order o in list)
        //    //{
        //    //    o.CustomerID = o.CustomerID ?? 0;
        //    //    o.PosID = o.PosID ?? 0;
        //    //}
        //    //IEnumerable<OrderItem> orderItems = info.GetAllOrderItem();
        //    CrystalReport1 rp = new CrystalReport1();
        //    //rp.SetDataSource(list);
        //    //rp.SetDataSource(orderItems);
            

        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();


        //    Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return File(stream, "application/pdf", "CustomerList.pdf");
        //}
    }
}