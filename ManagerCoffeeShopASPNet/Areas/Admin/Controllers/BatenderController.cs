using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using ManagerCoffeeShopASPNet.ManagerSession;
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

        /// <summary>
        /// lấy tất cả các orderitem cần pha chế
        /// </summary>
        /// <returns></returns>
        [Route("GetListOrderItemNeedPreparetion")]
        public ActionResult GetListOrderItemNeedPreparetion()
        {
            string status = "Pending";
            ListOrderItemGroupByFoodAndDrink listGroup = new ListOrderItemGroupByFoodAndDrink();
            IEnumerable<OrderItem> orderItems = info.GetAllOrderItemByStatus(status);
            //int index;
            //foreach(OrderItem item in orderItems)
            //{
            //    index = listGroup.FindIndexFoodAndDrinkInListGroup(item.FDID);
            //    if(index == -1)
            //    {
            //        OrderItemGroupByFoodAndDrink orderItemGroupByFoodAndDrink = new OrderItemGroupByFoodAndDrink();
            //        orderItemGroupByFoodAndDrink.FoodAndDrink = item.FoodAndDrink;
            //        orderItemGroupByFoodAndDrink.Quantity = item.Quantity;
            //        listGroup.list.Add(orderItemGroupByFoodAndDrink);
            //        int temp = listGroup.FindIndexFoodAndDrinkInListGroup(item.FDID);
            //        listGroup.list[temp].OrderItems.Add(item);
            //    }
            //    else
            //    {
            //        listGroup.list[index].Quantity = listGroup.list[index].Quantity + item.Quantity;
            //        listGroup.list[index].OrderItems.Add(item);
            //    }
            //}
            //return View(listGroup.list);
            return View(orderItems);
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
        public ActionResult UpdateReady(int OrderItemID, int OrderID, int NumOfOrderItem, string view)
        {
            string status = "Ready";
            bool result = info.UpdateStatus(OrderItemID, status);
            OrderItem oi = info.GetOrderItemByOrderItemID(OrderItemID);
            Recipe recipe = info.GetRecipeByFDID(oi.FDID);
            if (recipe != null)
            {
                IEnumerable<RecipeDetail> details = info.GetAllRecipeDetailByRecipeID(recipe.RecID);
                foreach (RecipeDetail detail in details)
                {
                    IngreExchange exchange = info.GetIngreExchangeByRecipeDetailID(detail.RecipeDetailID);
                    int IngreID = Convert.ToInt32(exchange.IngreID);
                    double AmountDiscount = (exchange.Amount / 20) * oi.Quantity;
                    Ingredient ingre = info.GetIngredientByIngreID(IngreID);
                    ingre.Amount = ingre.Amount - AmountDiscount;
                    info.UpdateIngredient(ingre);
                }
            }
            if (view == "GetListOrderItemNeedPreparetion")
            {
                return RedirectToAction("GetListOrderItemNeedPreparetion", "Batender");
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
            if (confirm == "true")
            {
                string statusOrderItem = "Cancel";
                string statusOrder = "Ready";
                bool result = info.UpdateStatus(OrderItemID, statusOrderItem);
                if (view == "GetListOrderItemNeedPreparetion")
                {
                    return RedirectToAction("GetListOrderItemNeedPreparetion", "Batender");
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
                if (view == "GetListOrderItemNeedPreparetion")
                    return RedirectToAction("GetListOrderItemNeedPreparetion", "Batender");
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
            if (recipe != null) // đã có công thức
            {
                IEnumerable<RecipeDetail> recipeDetails = info.GetAllRecipeDetailByRecipeID(recipe.RecID);
                int count = recipeDetails.Count<RecipeDetail>();
                //if (count == 0) // chưa có bước
                //{
                //    return RedirectToAction("ShowAllRecipeDetailByRecipeID", "Batender", new { RecipeID = recipe.RecID });
                //} 
                //else // đã có bước
                //{
                //    ViewData["RecipeID"] = recipe.RecID;
                //    ViewData["recipe"] = recipe;
                //    ViewData["recipeDetails"] = recipeDetails;
                //    return View();
                //}
                ViewData["RecipeID"] = recipe.RecID;
                ViewData["recipe"] = recipe;
                ViewData["recipeDetails"] = recipeDetails;
                return View();
            }
            else // chưa có công thức
            {
                ViewData["message"] = "Chưa có công thức pha chế cho loại đồ uống này";
                ViewData["FDID"] = FDID;
                return View();
            }
        }

        [Route("GetAllFoodAndDrink")]
        public ActionResult GetAllFoodAndDrink(string Search)
        {
            IEnumerable<FoodAndDrink> fds = info.GetAllFoodAndDrink();
            List<FoodAndDrink> list = fds.ToList();
            List<FoodAndDrink> temp = new List<FoodAndDrink>();
            if (!String.IsNullOrEmpty(Search))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name.ToLower().Contains(Search.ToLower()))
                    {
                        temp.Add(list[i]);
                    }
                }
                fds = (IEnumerable<FoodAndDrink>)temp;
            }
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
            HttpPostedFileBase ImagePath, string Size, string Type, double UnitPrice, string Currency)
        {
            var Image = ImagePath;
            string fileName = ImagePath.FileName;
            var path = Path.Combine(Server.MapPath("/Assets/resource/img/recype"), fileName);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int temp = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
                path = Path.Combine(Server.MapPath("/Assets/resource/img/recype"), fileName);
                temp++;
            }
            ImagePath.SaveAs(path);
            string imagePath = "/Assets/resource/img/recype/" + fileName;
            bool result = info.InsertFoodAndDrink(Name, Desc, imagePath, Size, Type, UnitPrice, Currency);
            return RedirectToAction("GetAllFoodAndDrink", "Batender");
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


        [Route("DoDeleteFoodAndDrink")]
        public ActionResult DoDeleteFoodAndDrink(int FDID)
        {
            info.DeleteFoodAndDrinkByFDID(FDID);
            return RedirectToAction("GetAllFoodAndDrink", "Batender");
        }


        [Route("DoEditFoodAndDrink")]
        public ActionResult DoEditFoodAndDrink(int FDID, string Name, string Desc,
            HttpPostedFileBase ImagePath, string Size, string Type, double UnitPrice, string Currency)
        {
            var Image = ImagePath;
            string fileName = ImagePath.FileName;
            var path = Path.Combine(Server.MapPath("/Assets/resource/img/recype"), fileName);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int temp = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
                path = Path.Combine(Server.MapPath("/Assets/resource/img/recype"), fileName);
                temp++;
            }
            ImagePath.SaveAs(path);
            string imagePath = "/Assets/resource/img/recype/" + fileName;
            FoodAndDrink fd = new FoodAndDrink();
            fd.FDID = FDID;
            fd.Name = Name;
            fd.Desc = Desc;
            fd.ImagePath = imagePath;
            fd.Size = Size;
            fd.Type = Type;
            fd.UnitPrice = UnitPrice;
            fd.Currency = Currency;
            bool result = info.EditFoodAndDrink(fd);
            return RedirectToAction("GetAllFoodAndDrink", "Batender");
        }

        [Route("CreateRecipe")]
        public ActionResult CreateRecipe(int FDID)
        {
            bool result = info.InsertRecipe(FDID);
            Recipe recipe = info.GetRecipeByFDID(FDID);
            return RedirectToAction("ReadRecipe", "Batender", new { FDID = FDID });
        }

        //[Route("ShowAllRecipeDetailByRecipeID")] // show bước làm
        //public ActionResult ShowAllRecipeDetailByRecipeID(int RecipeID)
        //{
        //    try
        //    {
        //        IEnumerable<RecipeDetail> recipeDetails = info.GetAllRecipeDetailByRecipeID(RecipeID);
        //        int count = recipeDetails.Count();
        //        if (count > 1)
        //        {
        //            Recipe rec = info.GetRecipeByRecipeID(RecipeID);
        //            return RedirectToAction("ReadRecipe", new { FDID = rec.FDID });
        //        }
        //        else
        //        {
        //            ViewData["RecipeID"] = RecipeID;
        //            RecipeDetail RD = info.GetAllRecipeDetailByrecipeID(RecipeID);
        //            Ingredient ingredient = info.GetIngredientByIngreID(RD.IngreID);

        //            ViewData["IngreID"] = ingredient.Name;
        //            return View(recipeDetails);
        //        }

        //    }
        //    catch
        //    {
        //        ViewData["RecipeID"] = RecipeID;
        //        return View();
        //    }

        /// <summary>
        /// Thêm bước làm
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <returns></returns>
        [Route("CreateRecipeDetail")]
        public ActionResult CreateRecipeDetail(int RecipeID)
        {
            ViewData["RecipeID"] = RecipeID;
            // Load tất cả nguyên vật liệu vào dropdown
            IEnumerable<Ingredient> ingredients = info.GetAllIngredient();
            List<SelectListItem> listIngedients = new List<SelectListItem>();
            foreach (Ingredient ingre in ingredients)
            {
                SelectListItem item = new SelectListItem { Text = ingre.Name, Value = ingre.IngreID.ToString() };
                listIngedients.Add(item);
            }
            ViewData["listIngedients"] = listIngedients;
            Recipe rec = info.GetRecipeByRecipeID(RecipeID);
            ViewData["FDID"] = rec.FDID;
            // Hiển thị mặc định bước tiếp theo
            int Step = info.CountRecipeDetailByRecipeID(RecipeID) + 1;
            ViewData["Step"] = Step;
            return View();
        }

        [HttpPost]
        [Route("DoCreateRecipeDetail")]
        public ActionResult DoCreateRecipeDetail(int RecipeID, int Step, int IngreID, double Amount,
            string Unit, string Desc, float AmountIngre, string UnitIngre, int Quantity)
        {
            int FDID = info.GetRecipeByRecipeID(RecipeID).FDID;
            bool result = info.InsertRecipeDetail(RecipeID, Step, IngreID, Amount, Unit, Desc);
            int RecipeDetailID = info.GetLastRecipeDetailID();

            IngreExchange ingreExchange = new IngreExchange();
            ingreExchange.RecipeDetailID = RecipeDetailID;
            ingreExchange.FDID = FDID;
            ingreExchange.IngreID = IngreID;
            ingreExchange.Amount = Amount;
            ingreExchange.Unit = UnitIngre;
            ingreExchange.Quantity = Quantity;
            bool result1 = info.InsertIngreExchange(ingreExchange);

            return RedirectToAction("ReadRecipe", "Batender", new { FDID = FDID });
        }

        [Route("SendMessageIngredientWithOut")]
        public ActionResult SendMessageIngredientWithOut()
        {
            List<Ingredient> ingres = info.GetAllIngredient().ToList();
            List<SelectListItem> listIngre = new List<SelectListItem>();
            foreach (Ingredient item in ingres)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.IngreID.ToString();
                select.Text = item.Name;
                listIngre.Add(select);
            }
            ViewData["listIngre"] = listIngre;
            return View();
        }

        [HttpPost]
        [Route("DoSendMessageIngredientWithOut")]
        public ActionResult DoSendMessageIngredientWithOut(int IngreID, double Amount, string Unit, string SendMessage)
        {

            ManagerSessionLogin session = new ManagerSessionLogin();
            bool result = false;
            if (session.GetCurrentEmployee() != null)
            {
                Employee em = session.GetCurrentEmployee();
                int EmployeeID = em.EmployeeID;
                result = info.InsertIngredientMessage(IngreID, EmployeeID, Amount, Unit, SendMessage);
                if (result == true)
                    TempData["message"] = "Gửi thành công";
                else TempData["error"] = "Gửi thất bại";
            }
            else
            {
                TempData["error"] = "Vui lòng đăng nhập với tài khoản admin";
                return RedirectToAction("Index", "Home", new { area = "Main" });
            }
            return RedirectToAction("SendMessageIngredientWithOut", "Batender");
        }
        [Route("EditRecipeDetail")]
        public ActionResult EditRecipeDetail(int RecipeDetailID)
        {
            RecipeDetail recipeDetail = info.GetRecipeDetailByRecipeDetailID(RecipeDetailID);
            List<SelectListItem> listIngredient = new List<SelectListItem>();
            IEnumerable<Ingredient> ingres = info.GetAllIngredient();
            foreach(var item in ingres)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.IngreID.ToString();
                select.Text = item.Name.ToString();
                listIngredient.Add(select);
            }
            ViewData["listIngreID"] = listIngredient;
            Recipe rec = info.GetRecipeByRecipeID(recipeDetail.RecID);
            IngreExchange exchange = info.GetIngreExchangeByRecipeDetailID(recipeDetail.RecipeDetailID);
            ViewData["exchange"] = exchange;
            ViewData["FDID"] = rec.FDID;
            return View(recipeDetail);
        }
        [Route("DoEditRecipeDetail")]
        public ActionResult DoEditRecipeDetail(int RecipeDetailID, int RecID, int Step, int IngreID, float Amount
            , string Unit, string Desc, float AmountIngre, string UnitIngre, int Quantity, int IngreExchangeID)
        {
            info.EditRecipeDetail(RecipeDetailID, Step, IngreID, Amount, Unit, Desc);
            IngreExchange exchange = info.GetIngreExchangeByIngreExchangeID(IngreExchangeID);
            exchange.Amount = AmountIngre;
            exchange.Unit = UnitIngre;
            exchange.Quantity = Quantity;
            exchange.IngreExchangeID = IngreExchangeID;
            info.EditIngreExchange(exchange);
            Recipe rec = info.GetRecipeByRecipeID(RecID);
            return RedirectToAction("ReadRecipe", "Batender", new { FDID = rec.FDID });
        }
        [Route("DeleteRecipeDetail")]
        public ActionResult DeleteRecipeDetail(int RecipeDetailID)
        {
            RecipeDetail recipedetail = info.GetRecipeDetailByRecipeDetailID(RecipeDetailID);
            Ingredient ingre = info.GetIngredientByIngreID(recipedetail.IngreID);
            ViewData["IngreID"] = ingre.Name;
            Recipe rec = info.GetRecipeByRecipeID(recipedetail.RecID);
            ViewData["FDID"] = rec.FDID;
            return View(recipedetail);
        }
        [Route("DoDeleteRecipeDetail")]
        public ActionResult DoDeleteRecipeDetail(int RecipeDetailID)
        {
            RecipeDetail recipedetail = info.GetRecipeDetailByRecipeDetailID(RecipeDetailID);
            Recipe rec = info.GetRecipeByRecipeID(recipedetail.RecID);
            info.DeleteRecipeDetail(RecipeDetailID);
            return RedirectToAction("ReadRecipe", "Batender", new { FDID = rec.FDID });
        }

        //[Route("CreateIngreExchangeForFoodAndDrink")]
        //public ActionResult CreateIngreExchangeForFoodAndDrink()
        //{
        //    return View();
        //}

        [Route("GetUnitByIngreID")]
        public ActionResult GetUnitByIngreID(int IngreID)
        {
            string Unit = info.GetIngredientByIngreID(IngreID).Unit;
            return Json(new { Unit = Unit }, JsonRequestBehavior.AllowGet);
        }

    }
}