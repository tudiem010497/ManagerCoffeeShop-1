using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Accountant")]
    public class AccountantController : Controller
    {
        private InformationAccountant info = new InformationAccountant();
        private InformationWeb infoWeb = new InformationWeb();
        // GET: Admin/Accountant
        public ActionResult Index()
        {
            return View();
        }
        [Route("GetAllReceiptWaitToConfirm")]
        public ActionResult GetAllReceiptWaitToConfirm()
        {
            IEnumerable<Receipt> receipt = info.GetAllReceipt();
            //IEnumerable<Receipt> receipt = info.GetReceiptWaitToConfirm();
            return View(receipt);
        }
        //xem danh sách chi tiết của phiếu nhập
        [Route("GetReceiptDetailByReceiptID")]
        public ActionResult GetReceiptDetailByReceiptID(int ReceiptID)
        {
            IEnumerable<ReceiptDetail> rec = info.GetReceiptDetailByReceiptID(ReceiptID);
            ViewData["ReceiptID"] = ReceiptID;
            return View(rec);
        }
        //nhấn nút duyệt phiếu nhập
        [Route("ConfirmAll")]
        public ActionResult ConfirmAll(int ReceiptID, string View)
        {
            string Status1 = "Confirm";
            int temp = 0;
            IEnumerable<Receipt> receipt = info.GetReceiptByReceiptID(ReceiptID);
            if (View == "GetAllReceiptWaitToConfirm")
            {
                info.UpdateReceipt(ReceiptID, Status1);
            }
            if (View == "GetReceiptDetailByReceiptID")
            {
                IEnumerable<ReceiptDetail> receiptDetail = info.GetReceiptDetailByReceiptID(ReceiptID);
                foreach (var item in receiptDetail)
                {
                    if (item.Status != Status1)
                    {
                        temp = 0;
                        break;
                    }
                    else
                    {
                        temp = 1;
                    }
                }
                if (temp == 1)
                {
                    info.UpdateReceipt(ReceiptID, Status1);
                }
            }
            return RedirectToAction("GetAllReceiptWaitToConfirm");
        }
        //nhấn nút không duyệt phiếu nhập
        [Route("CancelAll")]
        public ActionResult CancelAll(int ReceiptID)
        {
            string Status = "Cancel";
            IEnumerable<Receipt> receipt = info.GetReceiptByReceiptID(ReceiptID);
            info.UpdateReceipt(ReceiptID, Status);
            return RedirectToAction("GetAllReceiptWaitToConfirm");
        }
        //nhấn nút duyệt chi tiết phiếu nhập
        [Route("Confirm")]
        public ActionResult Confirm(int ReceiptDetailID)
        {
            ReceiptDetail receiptDetail = info.GetReceiptDetailByReceiptDetailID(ReceiptDetailID);
            info.UpdateReceiptDetail(ReceiptDetailID, "Confirm");
            return RedirectToAction("GetReceiptDetailByReceiptID", new { ReceiptID = receiptDetail.ReceiptID });
        }
        //nhấn nút không duyệt chi tiết phiếu nhập
        [Route("Cancel")]
        public ActionResult Cancel(int ReceiptDetailID)
        {
            ReceiptDetail receiptDetail = info.GetReceiptDetailByReceiptDetailID(ReceiptDetailID);
            info.UpdateReceiptDetail(ReceiptDetailID, "Cancel");
            return RedirectToAction("GetReceiptDetailByReceiptID", new { ReceiptID = receiptDetail.ReceiptID });
        }

        //Tạo phiếu lập bảng lương
        //Bước 1: chọn mã nhân viên cần tạo phiếu
        //[Route("CreatePayrollForEmployee")]
        //public ActionResult CreatePayrollForEmployee()
        //{
        //    IEnumerable<Employee> employees = infoWeb.GetAllEmployee();
        //    List<SelectListItem> listEmployee = new List<SelectListItem>();
        //    foreach (var item in employees)
        //    {
        //        SelectListItem selectID = new SelectListItem();
        //        selectID.Text = item.EmployeeID.ToString();
        //        listEmployee.Add(selectID);
        //    }
        //    ViewData["listEmployee"] = listEmployee;
        //    return View();
        //}
        //Bước 2: Điền thông tin vào phiếu tương ứng với mã nhân viên đã chọn
        [Route("CreatePayroll")]
        public ActionResult CreatePayroll(int EmployeeID)
        {
            Employee em = infoWeb.GetEmployeeByID(EmployeeID);
            Account acc = infoWeb.GetAccountByEmail(em.Email);
            BasicSalary basicSalary = infoWeb.GetBasicSalaryByEmployeeID(EmployeeID);
            Salary salary = infoWeb.GetSalaryBySalaryID(basicSalary.SalaryID);
            ViewData["EmployeeID"] = EmployeeID;
            ViewData["EmployeeName"] = em.Name;
            ViewData["Position"] = acc.AccType + " " + acc.Position;
            ViewData["SalaryType"] = salary.Type;
            ViewData["BasicSalary"] = salary.UnitPrice;
            return View();
        }
        //Bước 3: thực hiện tạo bảng lương
        [Route("DoCreatePayroll")]
        public ActionResult DoCreatePayroll(string json)
        {
            PayrollModel payrollModel = JsonConvert.DeserializeObject<PayrollModel>(json);
            info.InsertTimeSheet(payrollModel.EmployeeID, payrollModel.WorkDay,payrollModel.Total,"VND");
            TimeSheet timeSheet = info.GetTimeSheetByEmployeeID(payrollModel.EmployeeID);
            info.InsertTimeSheetDetail(timeSheet.TimeSheetID, payrollModel.Bonus, payrollModel.Penalty, "VND", payrollModel.Desc);
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}