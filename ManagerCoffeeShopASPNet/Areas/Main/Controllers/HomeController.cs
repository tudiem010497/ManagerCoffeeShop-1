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
            //IEnumerable<Blog> blogs = infomationIndex.GetBlog();
            ViewData["infoIndex"] = infoIndex;
            ViewData["menus"] = menus;
            ViewData["bannerImgs"] = bannerImgs;
            ViewData["fds"] = fds;
            //ViewData["blogs"] = blogs;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var result = new AccountModel().Login(model.UserName, model.Password);
            if (result && ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home", new { Areas = "Main" });
                //alert("Ban da dang nhap thanh cong");
            }
            else ModelState.AddModelError("", "Username or Password is incorrect");

            return View(model);
        }
    }
}