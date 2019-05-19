using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("web")]
    public class WebController : Controller
    {
        private InformationWeb info = new InformationWeb();
        // GET: Admin/Web
        public ActionResult Index()
        {
            return View();
        }
        [Route("GetAllEmployee")]
        public ActionResult GetAllEmployee(string search)
        {
            IEnumerable<Employee> employee = info.GetAllEmployee();
            List<Employee> list = employee.ToList();
            List<Employee> temp = new List<Employee>();
            if (!String.IsNullOrEmpty(search))
            {
                for(int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name.ToLower().Contains(search.ToLower()))
                    {
                        temp.Add(list[i]);
                    }
                }
                employee = (IEnumerable<Employee>)temp;
            }
            return View(employee);
        }
        [Route("GetFormAddNewEmployee")]
        public ActionResult GetFormAddNewEmployee()
        {
            IEnumerable<CoffeeShop> coffeeShop = info.GetAllCoffeeShop();
            List<SelectListItem> listCoffeeShop = new List<SelectListItem>();
            foreach(var item in coffeeShop)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.CSID.ToString();
                listCoffeeShop.Add(select);
            }
            ViewData["listCoffeeShop"] = listCoffeeShop;
            return View();
        }
        
        [Route("CreateEmployee")]
        public ActionResult CreateEmployee(string Name, string Email, string Address, string Phone, DateTime DOB, string Gender, string IndentityNum, string Status)
        {
            bool result = info.InsertEmployee( Name, Email, Address, Phone, DOB, Gender, IndentityNum, Status);
            int EmployeeID = info.GetLastEmployeeID();//lấy ra mã NV vừa tạo
            Employee em = info.GetEmployeeByID(EmployeeID); //lấy thông tin theo mã NV
            Salary salary = info.GetSalaryByDesc(em.Status);//lấy thông tin mức lương dựa vào status của nhân viên
            info.InsertBasicSalary(EmployeeID, salary.SalaryID);//thêm vào bảng BasicSalary 
            return RedirectToAction("GetAllEmployee", "Web");
        }
        [Route("DetailEmployee")]
        public ActionResult DetailEmployee(int EmployeeID)
        {
            IEnumerable<Employee> em = info.GetEmployeeByEmployeeID(EmployeeID);
            return View(em);
        }
        [Route("DeleteEmployee")]
        public ActionResult DeleteEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByID(EmployeeID);
            return View(em);
        }
        [Route("DoDeleteEmployee")]
        public ActionResult DoDeleteEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByID(EmployeeID);
            Account acc = info.GetAccountByEmail(em.Email);
            if (em.UserID != null)
            {
                bool result = info.DeleteEmployee(EmployeeID);
                bool resultAcc = info.DeleteAccount(acc.UserID);

                if (result == true && resultAcc == true)
                {
                    TempData["messageSuccess"] = "Xóa thành công";
                    return RedirectToAction("GetAllEmployee", "Web");
                }
                else
                {
                    TempData["messageFail"] = "Không thể xóa";
                    return RedirectToAction("GetAllEmployee", "Web");
                }
            }
            else
            {
                bool result = info.DeleteEmployee(EmployeeID);
                if (result == true)
                {
                    TempData["messageSuccess"] = "Xóa thành công";
                    return RedirectToAction("GetAllEmployee", "Web");
                }
                else
                {
                    TempData["messageFail"] = "Không thể xóa";
                    return RedirectToAction("GetAllEmployee", "Web");
                }
            }
        }
        [Route("EditEmployee")]
        public ActionResult EditEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByID(EmployeeID);
            IEnumerable<CoffeeShop> coffeeShop = info.GetAllCoffeeShop();
            List<SelectListItem> listCoffeeShop = new List<SelectListItem>();
            foreach (var item in coffeeShop)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.CSID.ToString();
                listCoffeeShop.Add(select);
            }
            ViewData["listCoffeeShop"] = listCoffeeShop;
            return View(em);
        }
        [Route("DoEditEmployee")]
        //int UserID,
        public ActionResult DoEditEmployee(int EmployeeID, int CSID, string Name, string Email, string Address, string Phone, DateTime DOB, string Gender, string IndentityNum, string Status)
        {
            Employee em = new Employee();
            em.EmployeeID = EmployeeID;
            //em.UserID = UserID;
            em.CSID = CSID;
            em.Name = Name;
            em.Email = Email;
            em.Address = Address;
            em.Phone = Phone;
            em.DOB = DOB;
            em.Gender = Gender;
            em.IndentityNum = IndentityNum;
            em.Status = Status;
            info.EditEmployee(em);
            Employee employee = info.GetEmployeeByID(EmployeeID);
            Salary salary = info.GetSalaryByDesc(em.Status);//lấy thông tin mức lương dựa vào status của nhân viên
            info.UpdateBasicSalary(EmployeeID, salary.SalaryID);
            return RedirectToAction("GetAllEmployee", "Web");
        }
        [Route("GetFormAddNewAccountForEmployee")]
        public ActionResult GetFormAddNewAccountForEmployee(int EmployeeID)
        {
            TempData["EmployeeID"] = EmployeeID;
            return View();
        }
        //[Route("CreateAccountForEmployee")]
        //public ActionResult CreateAccountForEmployee(string json)
        //{
        //    AccountModel test = JsonConvert.DeserializeObject<AccountModel>(json);
        //    Employee em = info.GetEmployeeByEmployeeID(test.EmployeeID);
        //    string UserName = test.UserName;
        //    string Password = test.Password;
        //    string AccType = test.AccType;
        //    string Position = test.Position;
        //    string fileName = test.Avatar;
        //    //HttpPostedFileBase Avatar = fileName;
        //    var path = Path.Combine(Server.MapPath("/Assets/resource/img/avatar"), fileName);
        //    string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
        //    string extension = Path.GetExtension(fileName);
        //    int temp = 1;
        //    while (System.IO.File.Exists(path))
        //    {
        //        fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
        //        path = Path.Combine(Server.MapPath("/Assets/resource/img/avatar/"), fileName);
        //        temp++;
        //    }
        //    //fileName.SaveAs(path);
        //    //Avatar.SaveAs(path);
        //    string imagePath = "/Assets/resource/img/avatar/" + fileName;
        //    bool result = info.InsertAccount(UserName, Password, em.Email, AccType, Position, imagePath);
        //    Account acc = info.GetAccountByEmail(em.Email);
        //    bool resutlEmployee = info.EditEmployeeMissingUserID(em.Email, acc.UserID);
        //    return Json(new { UserID = acc.UserID }, JsonRequestBehavior.AllowGet);
        //}
        [Route("CreateAccountForEmployee")]
        [HttpPost]
        public ActionResult CreateAccountForEmployee(int EmployeeID, string UserName, string Password,
            string AccType, string Position, HttpPostedFileBase Avatar)
        {
            Employee em = info.GetEmployeeByID(EmployeeID);
            var Image = Avatar;
            string fileName = Avatar.FileName;
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
            Avatar.SaveAs(path);
            string imagePath = "~/Assets/resource/img/avatar/" + fileName;
            bool result = info.InsertAccount(UserName, Password, em.Email, AccType, Position, imagePath);
            Account acc = info.GetAccountByEmail(em.Email);
            bool resultEmployee = info.EditEmployeeMissingUserID(em.Email, acc.UserID);
            return RedirectToAction("GetAllEmployee", "Web");
        }
        [Route("ViewAccountInformation")]
        public ActionResult ViewAccountInformation(int UserID)
        {
            IEnumerable<Account> acc = info.GetAccountByID(UserID);
            return View(acc);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetAllCoffeeShop")]
        public ActionResult GetAllCoffeeShop()
        {
            IEnumerable<CoffeeShop> coffeeshop = info.GetAllCoffeeShop();
            return View(coffeeshop);
        }
        [Route("GetAddNewCoffeeShop")]
        public ActionResult GetAddNewCoffeeShop()
        {
            return View();
        }
        [Route("CreateCoffeeShop")]
        public ActionResult CreateCoffeeShop(string Name, string Address, string Phone, HttpPostedFileBase LogoImagePath, string TitleAbout, string DescAbout, string TitleContact, string DescContact, string Email)
        {
            string fileName = LogoImagePath.FileName;
            var path = Path.Combine(Server.MapPath("~/Assets/resource/img/"), fileName);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int temp = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
                path = Path.Combine(Server.MapPath("~/Assets/resource/img/"), fileName);
                temp++;
            }
            LogoImagePath.SaveAs(path);
            string imagePath = "~/Assets/resource/img/" + fileName;
            bool result = info.InsertCoffeeShop(Name, Address, Phone, imagePath, TitleAbout, DescAbout, TitleContact, DescContact, Email);
            return RedirectToAction("GetAllCoffeeShop", "Web");
        }
        [Route("EditCoffeeShop")]
        public ActionResult EditCoffeeShop(int CSID)
        {
            IEnumerable<CoffeeShop> cs = info.GetCoffeeShopByCSID(CSID);
            return View(cs);
        }
        [Route("DoEditCoffeeShop")]
        public ActionResult DoEditCoffeeShop(int CSID, string Name, string Address, string Phone, HttpPostedFileBase LogoImagePath, string TitleAbout, string DescAbout, string TitleContact, string DescContact, string Email)
        {
            string fileName = LogoImagePath.FileName;
            var path = Path.Combine(Server.MapPath("~/Assets/resource/img/"), fileName);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int temp = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
                path = Path.Combine(Server.MapPath("~/Assets/resource/img/"), fileName);
                temp++;
            }
            LogoImagePath.SaveAs(path);
            string imagePath = "~/Assets/resource/img/" + fileName;
            CoffeeShop coffeeshop = new CoffeeShop();
            coffeeshop.CSID = CSID;
            coffeeshop.Name = Name;
            coffeeshop.Address = Address;
            coffeeshop.Phone = Phone;
            coffeeshop.LogoImagePath = imagePath;
            coffeeshop.TitleAbout = TitleAbout;
            coffeeshop.DescAbout = DescAbout;
            coffeeshop.TitleContact = TitleContact;
            coffeeshop.DescContact = DescContact;
            coffeeshop.Email = Email;
            bool result = info.EditCoffeeShop(coffeeshop);
            return RedirectToAction("GetAllCoffeeShop", "Web");
        }
        [Route("DetailsCoffeeShop")]
        public ActionResult DetailsCoffeeShop(int CSID)
        {
            IEnumerable<CoffeeShop> cs = info.GetCoffeeShopByCSID(CSID);
            return View(cs);
        }
    }
}