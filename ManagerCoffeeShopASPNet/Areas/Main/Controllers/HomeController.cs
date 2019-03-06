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

        InformationIndex infoIndex = new InformationIndex();
        // GET: Main/Home
        public ActionResult Index()
        {
            // IEnumerable<Account> accounts = infoIndex.GetInformationAccount();
            // ViewData["accounts"] = accounts;
            IEnumerable<FoodAndDrink> fds = infoIndex.GetInformationFoodAndDrink();
            ViewData["fds"] = fds;
            return View();
        }
    }
}