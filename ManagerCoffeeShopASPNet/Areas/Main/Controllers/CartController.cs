﻿using ManagerCoffeeShopASPNet.Areas.Main.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Main.Controllers
{
    [RouteArea("Main")]
    [RoutePrefix("Cart")]
    public class CartController : Controller
    {
        private InformationService info = new InformationService();
        private InformationWeb infoAccount = new InformationWeb();
        // GET: Main/Cart
        public ActionResult Cart()
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            ViewData["successUserID"] = Session["successUserID"];
            if (ViewData["successUserID"] != null)
            {
                int UserID = Convert.ToInt32(ViewData["successUserID"]);
                Account acc = infoAccount.GetAccountByUserID(UserID);
                ViewData["carts"] = carts;
                return View(acc);
            }
            else
            {
                ViewData["carts"] = carts;
                return View();
            }
        }

        [Route("AddToCart")]
        [HttpPost]
        public ActionResult AddToCart(string json)
        {
            Cart temp = JsonConvert.DeserializeObject<Cart>(json);
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Cart>();
            }
            List<Cart> list = Session["cart"] as List<Cart>;
            if (list.FirstOrDefault(m => m.FDID == temp.FDID) == null)
            {
                list.Add(temp);
            }
            else
            {
                Cart cart = list.FirstOrDefault(m => m.FDID == temp.FDID);
                var test = cart.Quantity + temp.Quantity;
                cart.Price = temp.Price;
                cart.Quantity = test;
                //cart.Total = test * temp.Price;
                //cart.Price = cart.Price * test;
            }
            Session["cart"] = list;
            
            return Json(new { FDID = temp.FDID, Name = temp.Name, Quantity = temp.Quantity, UnitPrice = temp.Total, Price = temp.Price }, JsonRequestBehavior.AllowGet);
        }
        /// controller này 
        public ActionResult EditQuantity(int FDID, int soluongmoi)
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            Cart fdEdit = carts.FirstOrDefault(m => m.FDID == FDID);
            if (fdEdit != null)
            {
                fdEdit.Quantity = soluongmoi;
            }
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteFD(int FDID)
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            Cart fdDelete = carts.FirstOrDefault(m => m.FDID == FDID);
            if (fdDelete != null)
            {
                carts.Remove(fdDelete);
            }
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteCart()
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            carts.Clear();
            return RedirectToAction("Cart");
        }
        [Route("Pay")]
        [HttpPost]
        public ActionResult Pay(string json)
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            int PosID = 1;
            double TotalAmount = 0;
            foreach(var item in carts)
            {
                TotalAmount = TotalAmount + item.Total;
            }
            bool resultOrder = info.InsertOrder(PosID, DateTime.Now, DateTime.Now, TotalAmount, "VND", "Delivery", "WaitToConfirm");
            int OrderID = info.GetLastOrderIDID();
            foreach(var item in carts)
            {
                info.InsertOrderItem(OrderID, item.FDID, item.Quantity, item.Desc, "WaitToConfirm");
            }
            int EmployeeID = 1;
            Cart temp = JsonConvert.DeserializeObject<Cart>(json);
            if (temp.UserID != 0)
            {
                info.InsertShipWithUserID(EmployeeID, temp.UserID, temp.CustName, DateTime.Now);
            }
            else
            {
                info.InsertShip(EmployeeID, temp.CustName, DateTime.Now);
            }
            Ship ship = info.GetShipByCustName(temp.CustName);
            info.InsertShipDetail(ship.ShipID, OrderID, temp.CustName, temp.Address, temp.Tel, "Not yet delivery");
            return Json(new { UserID = temp.UserID, CustName = temp.CustName, Tel = temp.Tel, Email = temp.Email, Address = temp.Address }, JsonRequestBehavior.AllowGet);
        }
    }
}
