﻿using ManagerCoffeeShopASPNet.Areas.Admin.Models;
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
            return View();
        }
        [Route("CreateEmployee")]
        public ActionResult CreateEmployee( string Name, string Email, string Address, string Phone, DateTime DOB, string Gender, string IndentityNum, string Status)
        {
            bool result = info.InsertEmployee( Name, Email, Address, Phone, DOB, Gender, IndentityNum, Status);
            return RedirectToAction("GetAllEmployee", "Web");
        }
        [Route("DetailEmployee")]
        public ActionResult DetailEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByEmployeeID(EmployeeID);
            return View(em);
        }
        [Route("DeleteEmployee")]
        public ActionResult DeleteEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByEmployeeID(EmployeeID);
            return View(em);
        }
        [Route("DoDeleteEmployee")]
        public ActionResult DoDeleteEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByEmployeeID(EmployeeID);
            Account acc = info.GetAccountByEmail(em.Email);
            //Account acc = info.GetAccountByUserID(em.UserID);
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
        [Route("EditEmployee")]
        public ActionResult EditEmployee(int EmployeeID)
        {
            Employee em = info.GetEmployeeByEmployeeID(EmployeeID);
            return View(em);
        }
        [Route("DoEditEmployee")]
        public ActionResult DoEditEmployee(int EmployeeID, int UserID, int CSID, string Name, string Email, string Address, string Phone, DateTime DOB, string Gender, string IndentityNum, string Status)
        {
            Employee em = new Employee();
            em.EmployeeID = EmployeeID;
            em.UserID = UserID;
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
            return RedirectToAction("GetAllEmployee", "Web");
        }
        [Route("GetFormAddNewAccountForEmployee")]
        public ActionResult GetFormAddNewAccountForEmployee(int EmployeeID)
        {
            TempData["EmployeeID"] = EmployeeID;
            return View();
        }
        [Route("CreateAccountForEmployee")]
        public ActionResult CreateAccountForEmployee(string json)
        {
            AccountModel test = JsonConvert.DeserializeObject<AccountModel>(json);
            Employee em = info.GetEmployeeByEmployeeID(test.EmployeeID);
            string UserName = test.UserName;
            string Password = test.Password;
            string AccType = test.AccType;
            string Position = test.Position;
            string fileName = test.Avatar;
            //HttpPostedFileBase Avatar = fileName;
            var path = Path.Combine(Server.MapPath("/Assets/resource/img/avatar"), fileName);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int temp = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
                path = Path.Combine(Server.MapPath("/Assets/resource/img/avatar/"), fileName);
                temp++;
            }
            //fileName.SaveAs(path);
            //Avatar.SaveAs(path);
            string imagePath = "/Assets/resource/img/avatar/" + fileName;
            bool result = info.InsertAccount(UserName, Password, em.Email, AccType, Position, imagePath);
            Account acc = info.GetAccountByEmail(em.Email);
            bool resutlEmployee = info.EditEmployeeMissingUserID(em.Email, acc.UserID);
            return Json(new { UserID = acc.UserID }, JsonRequestBehavior.AllowGet);
        }
        [Route("ViewAccountInformation")]
        public ActionResult ViewAccountInformation(int UserID)
        {
            Account acc = info.GetAccountByUserID(UserID);
            return View(acc);
        }
    }
}