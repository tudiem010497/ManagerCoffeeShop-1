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

        [Route("GetListOrder")]
        public ActionResult GetListOrder()
        {
            string status = "Pending";
            IEnumerable<Order> orders = info.GetAllOrderByStatus(status);
            return View(orders);
        }

        [Route("Detail")]
        public ActionResult DetailOrder(int OrderID)
        {
            string status = "Pending";
            IEnumerable<OrderItem> orderitems = info.GetAllOrderItemByOrderIDAndStatus(OrderID, status);
            return View(orderitems);
        }

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

        [Route("UpdateClosed")]
        public ActionResult UpdateClosed(int OrderItemID, int OrderID, int NumOfOrderItem, string confirm)
        {
            if(confirm == "true")
            {
                string status = "Closed";
                bool result = info.UpdateStatus(OrderItemID, status);
                if (NumOfOrderItem > 1)
                {
                    return RedirectToAction("DetailOrder", "Batender", new { OrderID = OrderID });
                }
                else
                {
                    bool resultUpdateStatusOrder = info.UpdateOrderStatus(OrderID, status);
                    return RedirectToAction("GetListOrder", "Batender");
                }
            }
            return RedirectToAction("DetailOrder", "Batender", new { OrderID = OrderID });
        }
    }
}