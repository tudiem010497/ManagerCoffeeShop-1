using ManagerCoffeeShopASPNet.Areas.Main.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Main.Controllers
{
    [RouteArea("main")]
    [RoutePrefix("cart")]
    public class CartController : Controller
    {
        // GET: Main/Cart
        public ActionResult Cart()
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            ViewData["carts"] = carts;
            return View();
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
        
    }
}
