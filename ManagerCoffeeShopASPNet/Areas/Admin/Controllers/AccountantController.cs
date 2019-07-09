using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Ajax.Utilities;
using ManagerCoffeeShopASPNet.Reporting;

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
                IEnumerable<ReceiptDetail> receiptDetail = info.GetReceiptDetailByReceiptID(ReceiptID);
                foreach(var item in receiptDetail)
                {
                    info.UpdateReceiptDetail(item.ReceiptDetailID, Status1);
                }
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
            IEnumerable<ReceiptDetail> receiptDetail = info.GetReceiptDetailByReceiptID(ReceiptID);
            foreach(var item in receiptDetail)
            {
                info.UpdateReceiptDetail(item.ReceiptDetailID, Status);
            }
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
            bool result = info.CheckTimeSheetOfEmployee(EmployeeID);
            if (result == false)
            {
                ViewData["WorkDay"] = 0;
            }
            else
            {
                TimeSheet timeSheet = info.GetTimeSheetByEmployeeID(EmployeeID);
                ViewData["WorkDay"] = timeSheet.TotalDay;
            }
            return View();
        }
        //Bước 3: thực hiện tạo bảng lương
        [Route("DoCreatePayroll")]
        public ActionResult DoCreatePayroll(string json)
        {
            PayrollModel payrollModel = JsonConvert.DeserializeObject<PayrollModel>(json);
            info.InsertPayroll(payrollModel.EmployeeID, payrollModel.EmployeeName, payrollModel.BasicSalary, payrollModel.WorkDay, payrollModel.Bonus, payrollModel.Penalty, payrollModel.Total, "VND", payrollModel.Desc, DateTime.Now);
            //TimeSheet timeSheet = info.GetTimeSheetByEmployeeID(payrollModel.EmployeeID);
            //info.InsertTimeSheetDetail(timeSheet.TimeSheetID, payrollModel.Bonus, payrollModel.Penalty, "VND", payrollModel.Desc);
            return Json(JsonRequestBehavior.AllowGet);
        }
        //Thêm bảng lương cho nhân viên bằng file excel 
        [Route("ImportExcelFileCreatePayrollForAllEmployee")]
        public ActionResult ImportExcelFileCreatePayrollForAllEmployee()
        {
            return View();
        }
        [HttpPost]
        [Route("DoImportExcelFileCreatePayrollForAllEmployee")]
        public ActionResult DoImportExcelFileCreatePayrollForAllEmployee(ImportExcel importExcel)
        {
            if (ModelState.IsValid)
            {
                string fileName = importExcel.file.FileName;
                string path = Path.Combine(Server.MapPath("~/Assets/Content/Upload/"), fileName);
                string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                int temp = 1;
                while (System.IO.File.Exists(path))
                {
                    fileName = fileNameNoExtension + "Copy(" + temp + ")" + extension;
                    path = Path.Combine(Server.MapPath("~/Assets/Content/Upload/"), fileName);
                    temp++;
                }
                importExcel.file.SaveAs(path);
                string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                //Sheet Name
                excelConnection.Open();
                string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                excelConnection.Close();
                //End
                OleDbCommand cmd = new OleDbCommand("Select * from [" + tableName + "]", excelConnection);
                excelConnection.Open();
                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["CoffeeShopDBConnectionString"].ConnectionString);
                //Give your Destination table name
                sqlBulk.DestinationTableName = "Payroll";
                //Mappings
                sqlBulk.ColumnMappings.Add("Date", "AddedOn");
                sqlBulk.ColumnMappings.Add("Desc", "Desc");
                sqlBulk.ColumnMappings.Add("BasicSalary", "BasicSalary");
                sqlBulk.ColumnMappings.Add("EmployeeName", "EmployeeName");
                sqlBulk.ColumnMappings.Add("EmployeeID", "EmployeeID");
                sqlBulk.ColumnMappings.Add("WorkDay", "WorkDay");
                sqlBulk.ColumnMappings.Add("Bonus", "Bonus");
                sqlBulk.ColumnMappings.Add("Penalty", "Penalty");
                sqlBulk.ColumnMappings.Add("Total", "Total");
                sqlBulk.ColumnMappings.Add("Currency", "Currency");
                sqlBulk.WriteToServer(dReader);
                excelConnection.Close();
                ViewData["ResultImportExcel"] = "Successfully Imported";
            }
            return RedirectToAction("ImportExcelFileCreatePayrollForAllEmployee");
        }
        //xem bảng lương của nhân viên dựa vào EmployeeID
        [Route("DetailPayrollOfEmployee")]
        public ActionResult DetailPayrollOfEmployee(/*int EmployeeID*/)
        {
            IEnumerable<Payroll> payroll = info.GetAllAddedOnOfPayroll();
            IEnumerable<Employee> em = infoWeb.GetAllEmployee();
            List<SelectListItem> listAddedOnPayroll = new List<SelectListItem>();
            List<SelectListItem> listEmployeeName = new List<SelectListItem>();
            foreach (var item in payroll.DistinctBy(p =>p.AddedOn))
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.AddedOn.ToString();
                listAddedOnPayroll.Add(select);
            }
            foreach (var item in em)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.Name.ToString();
                select.Value = item.EmployeeID.ToString();
                listEmployeeName.Add(select);
            }
            ViewData["listAddedOnPayroll"] = listAddedOnPayroll;
            ViewData["listEmployeeName"] = listEmployeeName;
            return View();
        }
        [Route("DoDetailPayrollOfEmployee")]
        public ActionResult DoDetailPayrollOfEmployee(int EmployeeName, DateTime AddedOn)
        {
            //Payroll p = JsonConvert.DeserializeObject<Payroll>(json);
            IEnumerable<Payroll> payroll = info.GetParyollByEmployeeIDAndAddedOn(EmployeeName, AddedOn);
            Payroll p = info.GetPayrollByEmployeeIDAndAddedOnNo(EmployeeName, AddedOn);
            if (p == null)
            {
                ViewData["payrollID"] = null;
            }
            else
            {
                ViewData["payrollID"] = p.PayrollID;
            }
           
            return View(payroll);
            //return Json(JsonRequestBehavior.AllowGet);
        }
        [Route("EditPayrollOfEmployee")]
        public ActionResult EditPayrollOfEmployee(int PayrollID)
        {
            Payroll payroll = info.GetPayrollByPayrollID(PayrollID);
            BasicSalary bs = infoWeb.GetBasicSalaryByEmployeeID(payroll.EmployeeID);
            Salary salary = infoWeb.GetSalaryBySalaryID(bs.SalaryID);
            ViewData["SalaryType"] = salary.Type;
            ViewData["PayrollID"] = PayrollID;
            return View(payroll);
        }
        [Route("DoEditPayrollOfEmployee")]
        public ActionResult DoEditPayrollOfEmployee(int PayrollID, int WorkDay, int Bonus, int Penalty, int Total, string Currency, string Desc)
        {

            bool result = info.EditPayroll(PayrollID, WorkDay, Bonus, Penalty, Total, Currency, Desc);
            if (result)
            {
                TempData["edit"] = "Sửa thành công";
            }
            return RedirectToAction("EditPayrollOfEmployee", new { PayrollID = PayrollID});
        }

        [Route("Report")]
        public ActionResult Report()
        {
            // Loại file xuất
            List<SelectListItem> ListExport = new List<SelectListItem>();
            SelectListItem pdf = new SelectListItem();
            pdf.Text = "Xuất file pdf";
            pdf.Value = "pdf";
            SelectListItem word = new SelectListItem();
            word.Text = "Xuất file word";
            word.Value = "word";
            ListExport.Add(pdf);
            ListExport.Add(word);
            ViewData["ListExport"] = ListExport;

            // Loại báo cáo
            List<SelectListItem> ListReportType = new List<SelectListItem>();
            SelectListItem GroupByFoodAndDrink = new SelectListItem();
            GroupByFoodAndDrink.Value = "ReportTurnOverGroupByFoodAndDrink";
            GroupByFoodAndDrink.Text = "Doanh thu theo loại đồ uống";
            SelectListItem GroupByTotalAmount = new SelectListItem();
            GroupByTotalAmount.Value = "ReportTurnOverGroupByMonth";
            GroupByTotalAmount.Text = "Tổng doanh thu";
            SelectListItem QuantityGroupByFoodAndDrink = new SelectListItem();
            QuantityGroupByFoodAndDrink.Value = "ReportQuantityGroupByFoodAndDrink";
            QuantityGroupByFoodAndDrink.Text = "Thống kê số lượng đồ uống";

            // Loại biểu đồ
            List<SelectListItem> ListMap = new List<SelectListItem>();
            SelectListItem Line = new SelectListItem();
            Line.Text = "Biểu đồ đường";
            Line.Value = "Line";
            SelectListItem Column = new SelectListItem();
            Column.Text = "Biểu đồ cột";
            Column.Value = "Column";
            SelectListItem Circle = new SelectListItem();
            Circle.Text = "Biểu đồ tròn";
            Circle.Value = "Circle";
            ListMap.Add(Line);
            ListMap.Add(Column);
            ListMap.Add(Circle);
            ViewData["ListMap"] = ListMap;

            ListReportType.Add(GroupByFoodAndDrink);
            ListReportType.Add(GroupByTotalAmount);
            ListReportType.Add(QuantityGroupByFoodAndDrink);

            ViewData["ListReportType"] = ListReportType;
            return View();
        }

        [Route("DoReport")]
        public ActionResult DoReport(string dateTimeFrom, string dateTimeTo, string ReportType, string ExportType,
            string MapType)
        {
            DateTime DateTimeFrom = Convert.ToDateTime(dateTimeFrom).AddHours(0).AddMinutes(0).AddSeconds(0);
            DateTime DateTimeTo = Convert.ToDateTime(dateTimeTo).AddHours(0).AddMinutes(0).AddSeconds(0);
            string FullName = string.Empty;
            if (Session["email"] != null)
            {
                Account acc = info.GetAccountByEmail(Convert.ToString(Session["email"]));
                FullName = acc.Employees.ElementAt(0).Name;
            }
            else FullName = "Phạm Thị Ngọc Hà";
            string tempFileName = string.Empty;
            string contentType = string.Empty;
            if(MapType == "Column") // Xuất biểu đồ cột
            {
                if (ExportType == "word")
                {
                    tempFileName = "doc";
                    contentType = "application/msword";
                }
                else
                {
                    tempFileName += "pdf";
                    contentType = "application/pdf";
                }
                if (ReportType == "ReportTurnOverGroupByFoodAndDrink")
                {
                    ReportTurnOverGroupByFoodAndDrink rp = new ReportTurnOverGroupByFoodAndDrink();
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    rp.SetParameterValue("FullName", FullName);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);
                }
                else if (ReportType == "ReportTurnOverGroupByMonth")
                {
                    ReportTurnOverGroupByMonth rp = new ReportTurnOverGroupByMonth();
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    rp.SetParameterValue("FullName", FullName);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);
                }
                else
                {
                    ReportQuantityGroupByFoodAndDrink rp = new ReportQuantityGroupByFoodAndDrink();
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    rp.SetParameterValue("FullName", FullName);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);

                }
            }
            else if(MapType == "Circle") // biểu đồ tròn
            {
                if (ExportType == "word")
                {
                    tempFileName = "doc";
                    contentType = "application/msword";
                }
                else
                {
                    tempFileName += "pdf";
                    contentType = "application/pdf";
                }
                if (ReportType == "ReportTurnOverGroupByFoodAndDrink")
                {
                    ReportTurnOverGroupByFoodAndDrinkCircle rp = new ReportTurnOverGroupByFoodAndDrinkCircle();
                    rp.SetParameterValue("FullName", FullName);
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);
                }
                else if (ReportType == "ReportTurnOverGroupByMonth")
                {
                    ReportTurnOverGroupByMonthCircle rp = new ReportTurnOverGroupByMonthCircle();
                    rp.SetParameterValue("FullName", FullName);
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);
                }
                else 
                {
                    ReportQuantityGroupByFoodAndDrinkCircle rp = new ReportQuantityGroupByFoodAndDrinkCircle();
                    rp.SetParameterValue("FullName", FullName);
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);

                }
                
            }
            else // biểu đồ đường
            {
                if (ExportType == "word")
                {
                    tempFileName = "doc";
                    contentType = "application/msword";
                }
                else
                {
                    tempFileName += "pdf";
                    contentType = "application/pdf";
                }
                if (ReportType == "ReportTurnOverGroupByFoodAndDrink")
                {
                    ReportTurnOverGroupByFoodAndDrinkLine rp = new ReportTurnOverGroupByFoodAndDrinkLine();
                    rp.SetParameterValue("FullName", FullName);
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);
                }
                else if (ReportType == "ReportTurnOverGroupByMonth")
                {
                    ReportTurnOverGroupByMonthLine rp = new ReportTurnOverGroupByMonthLine();
                    rp.SetParameterValue("FullName", FullName);
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);
                }
                else
                {
                    ReportQuantityGroupByFoodAndDrinkLine rp = new ReportQuantityGroupByFoodAndDrinkLine();
                    rp.SetParameterValue("FullName", FullName);
                    rp.SetParameterValue("@DateTimeFrom", DateTimeFrom);
                    rp.SetParameterValue("@DateTimeTo", DateTimeTo);
                    Stream stream;
                    if (ExportType == "word") stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    else stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, contentType, "Report." + tempFileName);

                }
            }
        }
    }
}