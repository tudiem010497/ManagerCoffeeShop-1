using ManagerCoffeeShopASPNet.Context;
using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using ManagerCoffeeShopASPNet.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerCoffeeShopASPNet.Areas.Main.Models;

namespace ManagerCoffeeShopASPNet.Areas.Main.Controllers
{
    [RouteArea("main")]
    [RoutePrefix("user")]
    public class HomeController : Controller
    {
        private InformationIndex infomationIndex = new InformationIndex();
        // GET: Main/Home
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Menu> menus = infomationIndex.GetMenu();
            IEnumerable<BannerImage> bannerImgs = infomationIndex.GetBannerImage();
            IEnumerable<InfoIndex> infoIndex = infomationIndex.GetInfoIndex();
            IEnumerable<FoodAndDrink> fds = infomationIndex.GetFoodAndDrink();
            //IEnumerable<Cart> cart = infomationIndex.GetCart();
            //IEnumerable<Blog> blogs = infomationIndex.GetBlog();
            ViewData["infoIndex"] = infoIndex;
            ViewData["menus"] = menus;
            ViewData["bannerImgs"] = bannerImgs;
            ViewData["fds"] = fds;
            //ViewData["cart"] = cart;
            //ViewData["warning"] = TempData["warning"];
            ViewData["error"] = TempData["error"];
            ViewData["success"] = TempData["success"];
            ViewData["SignUpOK"] = TempData["SignUpOK"];
            //ViewData["blogs"] = blogs;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var result = new AccountModel().Login(model.UserName, model.Password);
            var info = new AccountModel().InsertCustomer(model.Name, model.Email, model.Password);
            if (info == 1)
            {
                TempData["SignUpOK"] = "Congratulations on your successful registration. You can log in to your account.";
                return RedirectToAction("Index");
            }
            else
            {
                if (result && ModelState.IsValid)
                {
                    TempData["success"] = "Wellcome " + model.UserName;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Username or Password is incorrect. Please login again!";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}