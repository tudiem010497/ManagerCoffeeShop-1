﻿using System;
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
        // GET: Admin/Batender
        public ActionResult Index()
        {
            return View();
        }
        //[Route("AddOrder")]
    }
}