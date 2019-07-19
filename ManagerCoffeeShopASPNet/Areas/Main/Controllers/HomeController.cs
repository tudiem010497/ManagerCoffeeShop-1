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
using System.IO;

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
            //IEnumerable<Blog> blogs = infomationIndex.GetBlog();
            ViewData["infoIndex"] = infoIndex;
            ViewData["menus"] = menus;
            ViewData["bannerImgs"] = bannerImgs;
            ViewData["fds"] = fds;
            //ViewData["cart"] = cart;
            ViewData["successUserID"] = Session["successUserID"];
            ViewData["warning"] = TempData["warning"];
            ViewData["error"] = TempData["error"];
           
            ViewData["successCust"] = Session["successCust"];
            ViewData["SignUpOK"] = TempData["SignUpOK"];
            //ViewData["blogs"] = blogs;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public ActionResult Login(LoginModel model)
        {
            var result = new AccountModel().Login(model.Email, model.Password);
            if (result)
            {
                Account acc = infomationIndex.GetAccountByEmail(model.Email);
                
                if(acc.AccType != "Customer")
                {
                    Session["successEmployee"] = "Chào mừng " + acc.UserName;
                    Session["successUserID"] = acc.UserID.ToString();
                    Session["email"] = model.Email;
                    
                    return RedirectToAction("Service", "admin/service"/*, new { Areas = "Admin" }*/);
                }
                else
                {
                    Session["successCust"] = "Chào mừng  " + acc.UserName;
                    Session["successUserID"] = acc.UserID.ToString();
                    Session["email"] = model.Email;
                    return RedirectToAction("Index", "Home");
                }
                
            }
            else
            {
                TempData["error"] = "Tên đăng nhập hoặc mật khẩu sai. Vui lòng đăng nhập lại";
                return RedirectToAction("Index");
            }
        }
        [Route("Logout")]
        public ActionResult Logout(int UserID)
        {
            Session.Remove("success");
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SignUp")]
        public ActionResult SignUp(LoginModel model)
        {
            string fileName = model.Avatar.FileName;
            var path = Path.Combine(Server.MapPath("~/Assets/resource/img/avatar"), fileName);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int temp = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
                path = Path.Combine(Server.MapPath("~/Assets/resource/img/avatar"), fileName);
                temp++;
            }
            model.Avatar.SaveAs(path);
            string imagePath = "~/Assets/resource/img/avatar/" + fileName;
            //HttpPostedFileBase image = Convert.ChangeType( model.Avatar, HttpPostedFileBase);
            var info = new AccountModel().InsertCustomer(model.Name, model.Email, model.Password, imagePath);
            if (info == 1)
            {
                Account acc = infomationIndex.GetAccountByEmail(model.Email);
                int UserID = acc.UserID;
                bool result = infomationIndex.InsertCustomer(UserID, model.Name, model.DOB, model.Address, model.IdentityNum, model.Phone);
                TempData["SignUpOK"] = "Đăng nhập thành công. Bạn hãy đăng nhập vào tài khoản";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["warning"] = "Email đã tồn tại. Nhập email khác";
                return RedirectToAction("Index");
            }
        }
        [Route("ViewCustomerAccount")]
        public ActionResult ViewCustomerAccount(int UserID)
        {
            Account acc = infomationIndex.GetAccountByUserID(UserID);
            return View(acc);
        }
    }
}