using ManagerCoffeeShopASPNet.Context;
using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.DAOImpl;
using ManagerCoffeeShopASPNet.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Main.Controllers
{
    public class HomeController : Controller
    {

        private InformationIndex infomationIndex = new InformationIndex();
        // GET: Main/Home
        public ActionResult Index()
        {
            IEnumerable<Menu> menus = infomationIndex.GetMenu();
            IEnumerable<BannerImage> bannerImgs = infomationIndex.GetBannerImage();
            IEnumerable<InfoIndex> infoIndex = infomationIndex.GetInfoIndex();
            IEnumerable<FoodAndDrink> fds = infomationIndex.GetFoodAndDrink();
            ViewData["infoIndex"] = infoIndex;
            ViewData["menus"] = menus;
            ViewData["bannerImgs"] = bannerImgs;
            ViewData["fds"] = fds;
            return View();
        }
    }
}