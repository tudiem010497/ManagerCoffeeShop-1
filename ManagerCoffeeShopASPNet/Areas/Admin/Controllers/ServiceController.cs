﻿using CrystalDecisions.CrystalReports.Engine;
using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using ManagerCoffeeShopASPNet.Reporting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity.Infrastructure;
using CrystalDecisions.CrystalReports.Engine;
using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Infrastructure;
//using CrystalDecisions.CrystalReports.Engine;
using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ManagerCoffeeShopASPNet.ManagerSession;

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
        public ActionResult ServiceEmployee(string SearchString)
        {
            IEnumerable<FoodAndDrink> fds = info.GetFoodAndDrink();
            List<FoodAndDrink> list = fds.ToList();
            List<FoodAndDrink> temp = new List<FoodAndDrink>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name.ToLower().Contains(SearchString.ToLower()))
                    {
                        temp.Add(list[i]);
                    }
                }
                fds = (IEnumerable<FoodAndDrink>)temp;
            }
            IEnumerable<Position> positions = info.GetAllPosition();
            ViewData["positions"] = positions;
            ViewData["fds"] = fds;
            ViewData["successEmployee"] = Session["successEmployee"];
            ViewData["successUserID"] = Session["successUserID"];
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
            foreach (var item in OrderItemModel)
            {
                info.InsertOrderItem(OrderID, item.FoodAndDrinkID, item.Quantity, item.Desc, "Pending");
            }
            return Json(new { PosID = PosID }, JsonRequestBehavior.AllowGet);
        }

        //Xem danh sách hóa đơn đồ uống đặt online cần xác nhận (cập nhật trạng thái)
        [Route("GetAllOrderOnlineNeedConfirm")]
        public ActionResult GetAllOrderOnlineNeedConfirm()
        {
            string status = "WaitToConfirm";
            IEnumerable<Order> order = info.GetAllOrderByStatus(status);
            return View(order);
        }
        // Danh sách chi tiết đồ uống của hóa đơn online thực hiện việc xác nhận
        [Route("UpdateStatusOfOrderItemOnline")]
        public ActionResult UpdateStatusOfOrderItemOnline(int OrderID)
        {
            IEnumerable<OrderItem> orderItem = info.GetAllOrderItemByOrderID(OrderID);
            ViewData["OrderID"] = OrderID;
            return View(orderItem);
        }
        // nhấn nút xác nhận từng loại đồ uống của đơn online
        [Route("Confirm")]
        public ActionResult Confirm(int OrderItemID)
        {
            OrderItem orderItem = info.GetOrderItemByOrderItemID(OrderItemID);
            info.UpdateOrderItemStatus(OrderItemID, "Pending");
            return RedirectToAction("UpdateStatusOfOrderItemOnline", new { OrderID = orderItem.OrderID });
        }
        //nhấn nút hủy từng loại đồ uống của đơn online
        [Route("Cancel")]
        public ActionResult Cancel(int OrderItemID)
        {
            OrderItem orderItem = info.GetOrderItemByOrderItemID(OrderItemID);
            info.UpdateOrderItemStatus(OrderItemID, "Cancel");
            return RedirectToAction("UpdateStatusOfOrderItemOnline", new { OrderID = orderItem.OrderID });
        }
        //nhấn nút xác nhận toàn bộ đơn hàng
        [Route("ConfirmAll")]
        public ActionResult ConfirmAll(int OrderID)
        {
            IEnumerable<OrderItem> orderitem = info.GetAllOrderItemByOrderID(OrderID);
            int temp=0;
            string status1 = "Cancel";
            string status2 = "Pending";
            foreach (var item in orderitem)
            {
                if(item.Status != "Cancel")
                {
                    temp = 0;
                    break;
                }
                else temp = 1;
            }
            if (temp == 1)
            {
                info.UpdateOrderStatus(OrderID, status1);
            }
            if (temp == 0)
            {
                info.UpdateOrderStatus(OrderID, status2);
            }
            return RedirectToAction("GetAllOrderOnlineNeedConfirm");
        }
        //Danh sách hóa đơn đồ uống đã làm xong cần giao hàng (Desc = "Delivery", Status = "Ready")
        [Route("GetAllOrderNeedDelivery")]
        public ActionResult GetAllOrderNeedDelivery()
        {
            string Desc = "Delivery", Status = "Ready";
            IEnumerable<Order> order = info.GetAllOrderByDescAndStatus(Desc, Status);
            return View(order);
        }
        // nhấn nút xác nhận giao hàng 
        //status order cập nhật thành "Closed", status ShipDetail cập nhật thành "Delivery"(đang giao)
        [Route("ConfirmDelivery")]
        public ActionResult ConfirmDelivery(int OrderID)
        {
            string Status1 = "Closed", Status2 = "Delivery";
            IEnumerable<Order> order = info.GetOrderByOrderID(OrderID);
            info.UpdateOrderStatus(OrderID, Status1);
            info.UpdateShipDetailStatus(OrderID, Status2);
            return RedirectToAction("GetAllOrderNeedDelivery");
        }
        //Danh sách các đơn hàng online đang giao và sẽ được cập nhật "Close" trong ShipDetail khi giao thành công (NV giao hàng)
        [Route("GetListShipDelivery")]
        public ActionResult GetListShipDelivery()
        {
            string status = "Delivery";
            //IEnumerable<ShipDetail> ship = info.GetListShipDelivery();
            IEnumerable<ShipDetail> ship = info.GetShipDeliveryByStatus();
            return View(ship);
        }
        //nhấn nút xác nhận giao hàng thành công, cập nhật là "Close"
        [Route("ConfirmDeliveried")]
        public ActionResult ConfirmDeliveried(int ShipDetailID)
        {
            string Status = "Close";
            ShipDetail ship = info.GetShipDeliveryByShipDetailID(ShipDetailID);
            info.UpdateShipDetailStatusByShipDetailID(ShipDetailID, Status);
            return RedirectToAction("GetListShipDelivery", new { ShipDetailID = ship.ShipDetailID});
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
        [Route("DetailOrderNeedSevice")]
        public ActionResult DetailOrderNeedSevice(int OrderID)
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
            if (orderItem.Status == "Cancel")
            {
                bool result = info.UpdateOrderItemStatus(OrderItemID, status1);
            }
            else
            {
                info.UpdateOrderItemStatus(OrderItemID, status2);
            }

            // Nếu đang ở view Chi tiết hóa đơn 
            if(View == "DetailOrderNeedSevice")
            {
                // Nếu chi tiết hóa đơn != 0 thì redirect về DetailOrderNeedSevice ngược lại thì redirect về GetListOrderServiceGroupByOrder
                if (info.GetAllOrderItemByOrderIDAndNeedService(OrderID).Count() != 0)
                {
                    return RedirectToAction("DetailOrderNeedSevice", "Service", new { OrderID = OrderID });
                }
                else
                {
                    info.UpdateOrderStatus(OrderID, status1);
                    return RedirectToAction("GetListOrderServiceGroupByOrder", "Service");
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
            if (confirm == "true")
            {
                IEnumerable<OrderItem> orderItems = info.GetAllOrderItemByOrderID(OrderID);
                foreach (OrderItem orderItem in orderItems)
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

        [Route("PrintReport")]
        public ActionResult PrintReport()
        {
            CrystalReport1 rp = new CrystalReport1();
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //rp.SetDatabaseLogon("Diem", "", "DESKTOP-HA2TCUF", "CoffeeShopDB", false);
            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");
        }

        [Route("GetAllOrder")]
        public ActionResult GetAllOrder()
        {
            IEnumerable<Order> orders = info.GetAllOrder();
            return View(orders);
        }
        [Route("PrintOrder")]
        public ActionResult PrintOrder(int OrderID)
        {
            ExportOrderCustomer rp = new ExportOrderCustomer();
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //rp.SetDatabaseLogon("Diem", "", "DESKTOP-HA2TCUF", "CoffeeShopDB", false);
            rp.SetParameterValue("@OrderID", OrderID);
            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "OrderCustomer.pdf");
        }
        [Route("DetailOrder")]
        public ActionResult DetailOrder(int OrderID)
        {
            IEnumerable<OrderItem> orderitems = info.GetAllOrderItemByOrderID(OrderID);
            return View(orderitems);
        }
        [Route("ViewEmployeeAccount")]
        public ActionResult ViewEmployeeAccount(int UserID)
        {
            Account acc = info.GetAccountByUserID(UserID);
            return View(acc);
        }
    }
}