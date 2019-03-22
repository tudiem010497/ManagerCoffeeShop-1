using ManagerCoffeeShopASPNet.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("batender")]
    public class BatenderController : Controller
    {
        private InformationBatender info = new InformationBatender();

        // GET: Admin/Batender
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lấy danh sách đồ uống cần pha chế
        /// </summary>
        /// <returns></returns>
        [Route("GetListOrder")]
        public ActionResult GetListOrder()
        {
            string status = "Pending";
            IEnumerable<Order> orders = info.GetAllOrderByStatus(status);
            return View(orders);
        }

        /// <summary>
        /// Xem chi tiết pha chế
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [Route("Detail")]
        public ActionResult DetailOrder(int OrderID)
        {
            string status = "Pending";
            IEnumerable<OrderItem> orderitems = info.GetAllOrderItemByOrderIDAndStatus(OrderID, status);
            return View(orderitems);
        }

        /// <summary>
        /// Cập nhật thông tin pha chế là đã hoàn thành
        /// </summary>
        /// <param name="OrderItemID"></param>
        /// <param name="OrderID"></param>
        /// <param name="NumOfOrderItem"></param>
        /// <returns></returns>
        [Route("UpdateReady")]
        public ActionResult UpdateReady(int OrderItemID,int OrderID, int NumOfOrderItem)
        {
            string status = "Ready";
            bool result = info.UpdateStatus(OrderItemID, status);
            if(NumOfOrderItem > 1)
            {
                return RedirectToAction("DetailOrder", "Batender", new { OrderID = OrderID });
            }
            else
            {
                bool resultUpdateStatusOrder = info.UpdateOrderStatus(OrderID, status);
                return RedirectToAction("GetListOrder", "Batender");
            }
        }

        /// <summary>
        /// Cập nhật thông tin pha chế là hủy
        /// </summary>
        /// <param name="OrderItemID"></param>
        /// <param name="OrderID"></param>
        /// <param name="NumOfOrderItem"></param>
        /// <param name="confirm"></param>
        /// <returns></returns>
        [Route("UpdateClosed")]
        public ActionResult UpdateClosed(int OrderItemID, int OrderID, int NumOfOrderItem, string confirm)
        {
            if(confirm == "true")
            {
                string statusOrderItem = "Cancel";
                string statusOrder = "Ready";
                bool result = info.UpdateStatus(OrderItemID, statusOrderItem);
                if (NumOfOrderItem > 1)
                {
                    return RedirectToAction("DetailOrder", "Batender", new { OrderID = OrderID });
                }
                else
                {
                    bool resultUpdateStatusOrder = info.UpdateOrderStatus(OrderID, statusOrder);
                    return RedirectToAction("GetListOrder", "Batender");
                }
            }
            return RedirectToAction("DetailOrder", "Batender", new { OrderID = OrderID });
        }
    }
}