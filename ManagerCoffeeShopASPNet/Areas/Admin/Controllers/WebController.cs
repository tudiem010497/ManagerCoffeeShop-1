using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerCoffeeShopASPNet.Class;

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
            //ConvertStatusEmployee test = new ConvertStatusEmployee();
            //foreach(var item in employee)
            //{
            //    string a = test.ConvertStatus(item.Status).ToString();
            //    ViewData["test"] = a;
            //}
            
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
                select.Value = item.CSID.ToString();
                select.Text = item.Name.ToString();
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
            ViewData["EmployeeID"] = EmployeeID;
            ViewData["Status"] = em.FirstOrDefault().Status;
            return View(em);
        }
        [Route("DeleteEmployee")]
        public ActionResult DeleteEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByID(EmployeeID);
            ViewData["EmployeeID"] = EmployeeID;
            return View(em);
        }
        [Route("DoDeleteEmployee")]
        public ActionResult DoDeleteEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByID(EmployeeID);
            Account acc = info.GetAccountByEmail(em.Email);
            if (em.UserID != null)
            {
                bool resultBasicSalary = info.DeleteBasicSalary(EmployeeID);
                bool result = info.DeleteEmployee(EmployeeID);
                bool resultAcc = info.DeleteAccount(acc.UserID);

                if (result == true && resultAcc == true && resultBasicSalary==true)
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
                bool resultBasicSalary = info.DeleteBasicSalary(EmployeeID);
                bool result = info.DeleteEmployee(EmployeeID);
                if (result == true && resultBasicSalary==true)
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
            ViewData["EmployeeID"] = EmployeeID;
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
            bool result = info.CheckBasicSalaryOfEmployee(EmployeeID);
            if (result==false)
            {
                info.InsertBasicSalary(EmployeeID, salary.SalaryID);
            }
            info.UpdateBasicSalary(EmployeeID, salary.SalaryID);
            return RedirectToAction("DetailEmployee", "Web", new { EmployeeID = EmployeeID });
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
            ViewData["CSID"] = CSID;
            return View(cs);
        }
        [Route("DeleteCoffeeShop")]
        public ActionResult DeleteCoffeeShop(int CSID)
        {
            CoffeeShop cs = info.GetCoffeeShopByID(CSID);
            return View(cs);
        }
        [Route("DoDeleteCofffeeShop")]
        public ActionResult DoDeleteCofffeeShop(int CSID)
        {
            info.DeleteCoffeeShop(CSID);
            return RedirectToAction("GetAllCoffeeShop", "Web");
        }
        [Route("CreateDiagram")]
        public ActionResult CreateDiagram(float width, float height, float ratio, string FloorID)
        {
            IEnumerable<CoffeeShop> coffeeShop = info.GetAllCoffeeShop();
            List<SelectListItem> listCoffeeShop = new List<SelectListItem>();
            foreach (var item in coffeeShop)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.Name;
                select.Value = item.CSID.ToString();
                listCoffeeShop.Add(select);
            }
            ViewData["listCoffeeShop"] = listCoffeeShop;
            ViewData["width"] = width;
            ViewData["height"] = height;
            ViewData["ratio"] = ratio;
            ViewData["FloorID"] = FloorID;
            return View();
        }

        [HttpPost]
        [Route("SaveDiagram")]
        public ActionResult SaveDiagram(string json)
        {
            DiagramUpdate diagram = JsonConvert.DeserializeObject<DiagramUpdate>(json);
            float widthDiagram = diagram.widthDiagram;
            float heightDiagram = diagram.heightDiagram;
            float ratioDiagram = diagram.ratioDiagram;
            string FloorID = diagram.FloorID;
            int CSID = diagram.CSID;
            List<ImageDiagram> ListImageDiagram = diagram.ListImageDiagram;
            bool result = info.InsertCoffeeLandScape(CSID, FloorID, ratioDiagram, widthDiagram, heightDiagram);
            int CoffeeLandScapeID = info.GetLastCoffeeLandScapeID();
            foreach(ImageDiagram image in ListImageDiagram)
            {
                result = info.InsertCoffeeLandScapeDetail(CoffeeLandScapeID, image.Href, image.x, image.y, image.width, image.height, image.rotate);
            }
            return Json(JsonRequestBehavior.AllowGet); 
           
        }
        [Route("ViewDiagram")]
        public ActionResult ViewDiagram(int CLSID)
        {
            ViewData["reset"] = TempData["reset"];
            string url = Request.Url.AbsoluteUri;
            int end = url.IndexOf("/admin");
            string domain = url.Substring(0, end);
            IEnumerable<CoffeeLandScape> coffeeLandScapes = info.GetAllCoffeeLandScape();
            if(coffeeLandScapes.Count() != 0)
            {
                List<SelectListItem> listCoffeeLandScape = new List<SelectListItem>();
                foreach (var coffeelandscape in coffeeLandScapes)
                {
                    SelectListItem select = new SelectListItem();
                    select.Text = coffeelandscape.CLSID.ToString();
                    select.Value = coffeelandscape.CLSID.ToString();
                    listCoffeeLandScape.Add(select);
                }
                ViewData["listCoffeeLandScape"] = listCoffeeLandScape;
            }
            IEnumerable<CoffeeShop> coffeeShop = info.GetAllCoffeeShop();
            List<SelectListItem> listCoffeeShop = new List<SelectListItem>();
            foreach (var item in coffeeShop)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.Name;
                select.Value = item.CSID.ToString();
                listCoffeeShop.Add(select);
            }
            ViewData["listCoffeeShop"] = listCoffeeShop;
            CoffeeLandScape landscape = info.GetCoffeeLandScapeByID(CLSID);
            IEnumerable<CoffeeLandScapeDetail> detailLandScapes = info.GetAllCoffeeLandScapeDetailByCoffeeLandScapeID(CLSID);
            ViewData["landscape"] = landscape;
            ViewData["detailLandScapes"] = detailLandScapes;
            ViewData["domain"] = domain;
            //ViewData["CLSID"] = CLSID;
            return View();
        }
        [Route("DoViewDiagram")]
        public ActionResult DoViewDiagram(int CLSID)
        {
            //if(TempData["width"] != null)
            //{
            //    TempData["width"] = TempData["width"];
            //    TempData["height"] = TempData["height"];
            //    TempData["ratio"] = TempData["ratio"];
            //    TempData["CSID"] = TempData["CSID"];
            //    TempData["FloorID"] = TempData["FloorID"];
            //}
            
            return RedirectToAction("ViewDiagram", "Web", new { CLSID = CLSID });
        }

        [HttpPost]
        [Route("ResetDiagram")]
        public ActionResult ResetDiagram(int CLSID, float width, float height, float ratio,int CSID, string FloorID)
        {
            TempData["reset"] = "true";
            bool result = info.UpdateCoffeeLandScape(CLSID, width, height, ratio, CSID, FloorID);
            return Json(new { CLSID = CLSID });
        }

        [Route("SaveEditDiagram")]
        public ActionResult SaveEditDiagram(string json)
        {
            DiagramUpdate diagram = JsonConvert.DeserializeObject<DiagramUpdate>(json);
            float widthDiagram = diagram.widthDiagram;
            float heightDiagram = diagram.heightDiagram;
            float ratioDiagram = diagram.ratioDiagram;
            string FloorID = diagram.FloorID;
            int CSID = diagram.CSID;
            int CLSID = diagram.CLSID;
            List<ImageDiagram> ListImageDiagram = diagram.ListImageDiagram;
            bool result = info.UpdateCoffeeLandScape(CLSID, widthDiagram, heightDiagram, ratioDiagram, CSID, FloorID);
            foreach (ImageDiagram image in ListImageDiagram)
            {
                if (info.CheckCoffeeLandScapeDetailIsExistsByID(image.ID))
                {
                    result = info.UpdateCoffeeLandScapeDetail(image.ID, CLSID, image.Href, image.x, image.y, image.width, image.height, image.rotate);
                }
                else
                {
                    result = info.InsertCoffeeLandScapeDetail(CLSID, image.Href, image.x, image.y, image.width, image.height, image.rotate);
                }
            }
            //xóa image trong db
            List<int> intListID = new List<int>();
            List<int> ItemID = new List<int>();
            IEnumerable<CoffeeLandScapeDetail> detail = info.GetAllCoffeeLandScapeDetailByCoffeeLandScapeID(CLSID);
            foreach (var imagedelete in detail)
            {
                ItemID.Add(imagedelete.ItemID);
            }

            foreach (ImageDiagram temp in ListImageDiagram)
            {
                intListID.Add(temp.ID);
            }
            IEnumerable<int> differenceTwoList = ItemID.Except(intListID);
            foreach(var temp in differenceTwoList)
            {
                info.DeleteCoffeeLandScapeDetail(temp);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        [Route("DeleteDiagram")]
        public ActionResult DeleteDiagram(string json)
        {
            DiagramUpdate diagram = JsonConvert.DeserializeObject<DiagramUpdate>(json);
            IEnumerable<CoffeeLandScapeDetail> details = info.GetAllCoffeeLandScapeDetailByCoffeeLandScapeID(diagram.CLSID);
            foreach(var item in details)
            {
                info.DeleteCoffeeLandScapeDetail(item.ItemID);
            }
            bool result = info.DeleteCoffeeLandScape(diagram.CLSID);

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}