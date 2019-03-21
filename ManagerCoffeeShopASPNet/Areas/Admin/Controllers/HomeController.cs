using ManagerCoffeeShopASPNet.DAO;
using ManagerCoffeeShopASPNet.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("manager")]
    public class HomeController : Controller
    {
        InformationBatender info = new InformationBatender();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        
    }
}