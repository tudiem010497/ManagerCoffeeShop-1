using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Lấy danh sách đồ uống cần pha chế theo hóa đơn
        /// </summary>
        /// <returns></returns>
        [Route("GetListOrder")]
        public ActionResult GetListOrder()
        {
            string status = "Pending";
            IEnumerable<Order> orders = info.GetAllOrderByStatus(status);
            return View(orders);
        }

        [Route("GetListOrderItemGroupByFoodAndDrink")]
        public ActionResult GetListOrderItemGroupByFoodAndDrink()
        {
            string status = "Pending";
            ListOrderItemGroupByFoodAndDrink listGroup = new ListOrderItemGroupByFoodAndDrink();
            IEnumerable<OrderItem> orderItems = info.GetAllOrderItemByStatus(status);
            int index;
            foreach(OrderItem item in orderItems)
            {
                index = listGroup.FindIndexFoodAndDrinkInListGroup(item.FDID);
                if(index == -1)
                {
                    OrderItemGroupByFoodAndDrink orderItemGroupByFoodAndDrink = new OrderItemGroupByFoodAndDrink();
                    orderItemGroupByFoodAndDrink.FoodAndDrink = item.FoodAndDrink;
                    orderItemGroupByFoodAndDrink.Quantity = item.Quantity;
                    listGroup.list.Add(orderItemGroupByFoodAndDrink);
                    int temp = listGroup.FindIndexFoodAndDrinkInListGroup(item.FDID);
                    listGroup.list[temp].OrderItems.Add(item);
                }
                else
                {
                    listGroup.list[index].Quantity = listGroup.list[index].Quantity + item.Quantity;
                    listGroup.list[index].OrderItems.Add(item);
                }
            }
            return View(listGroup.list);
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
        public ActionResult UpdateReady(int OrderItemID,int OrderID, int NumOfOrderItem, string view)
        {
            string status = "Ready";
            bool result = info.UpdateStatus(OrderItemID, status);
            if(view == "GetListOrderItemGroupByFoodAndDrink")
            {
                return RedirectToAction("GetListOrderItemGroupByFoodAndDrink", "Batender");
            }
            else
            {
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
        }

        /// <summary>
        /// Cập nhật thông tin pha chế là hủy
        /// </summary>
        /// <param name="OrderItemID"></param>
        /// <param name="OrderID"></param>
        /// <param name="NumOfOrderItem"></param>
        /// <param name="confirm"></param>
        /// <returns></returns>
        [Route("UpdateCancel")]
        public ActionResult UpdateCancel(int OrderItemID, int OrderID, int NumOfOrderItem, string confirm, string view)
        {
            if(confirm == "true")
            {
                string statusOrderItem = "Cancel";
                string statusOrder = "Ready";
                bool result = info.UpdateStatus(OrderItemID, statusOrderItem);
                if(view == "GetListOrderItemGroupByFoodAndDrink")
                {
                    return RedirectToAction("GetListOrderItemGroupByFoodAndDrink", "Batender");
                }
                else
                {
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
            }
            else
            {
                if(view == "GetListOrderItemGroupByFoodAndDrink")
                    return RedirectToAction("GetListOrderItemGroupByFoodAndDrink", "Batender");
                else
                    return RedirectToAction("DetailOrder", "Batender", new { OrderID = OrderID });
            }
        }
        /// <summary>
        /// Xem công thức pha chế
        /// </summary>
        /// <param name="FDID"></param>
        /// <returns></returns>
        [Route("ReadRecipe")]
        public ActionResult ReadRecipe(int FDID)
        {
            Recipe recipe = info.GetRecipeByFDID(FDID);
            IEnumerable<RecipeDetail> recipeDetails = info.GetAllRecipeDetailByRecipeID(recipe.RecID);
            ViewData["recipe"] = recipe;
            ViewData["recipeDetails"] = recipeDetails;
            return View();
        }

        [Route("GetAllFoodAndDrink")]
        public ActionResult GetAllFoodAndDrink()
        {
            IEnumerable<FoodAndDrink> fds = info.GetAllFoodAndDrink();
            return View(fds);
        }

        [Route("GetFormAddNewFoodAndDrink")]
        public ActionResult GetFormAddNewFoodAndDrink()
        {
            return View();
        }

        [Route("CreateFoodAndDrink")]
        [HttpPost]
        public ActionResult CreateFoodAndDrink(string Name, string Desc,
            HttpPostedFileBase ImagePath,string Size, string Type, double UnitPrice, string Currency )
        {
            var Image = ImagePath;
            string fileName = ImagePath.FileName;
            var path = Path.Combine(Server.MapPath("~/Assets/resource/img/recype"), fileName);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int temp = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = fileNameNoExtension + "Copy(" + temp + ")"  + extension;
                path = Path.Combine(Server.MapPath("~/Assets/resource/img/recype"), fileName);
                temp++;
            }
            ImagePath.SaveAs(path);
            string imagePath = "~/Assets/resource/img/recype/" + fileName;
            bool result = info.InsertFoodAndDrink(Name, Desc, imagePath, Size, Type, UnitPrice, Currency);
            return RedirectToAction("GetFormAddNewFoodAndDrink", "Batender");
        }

        [Route("EditFoodAndDrink")]
        public ActionResult EditFoodAndDrink(int FDID)
        {
            FoodAndDrink fd = info.GetFoodAndDrinkByFDID(FDID);
            return View(fd);
        }

        [Route("DeleteFoodAndDrink")]
        public ActionResult DeleteFoodAndDrink(int FDID)
        {
            FoodAndDrink fd = info.GetFoodAndDrinkByFDID(FDID);
            return View(fd);
        }
    }
}