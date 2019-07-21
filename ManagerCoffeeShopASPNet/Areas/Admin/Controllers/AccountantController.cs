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
using ManagerCoffeeShopASPNet.Class;
using Microsoft.Office.Interop.Excel;

using System.Data;

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
            //IEnumerable<Receipt> receipt = info.GetAllReceipt();
            IEnumerable<Receipt> receipt = info.GetReceiptWaitToConfirm();
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
        public ActionResult ConfirmAll(int ReceiptID/*, string View*/)
        {
            string Status1 = "Confirm";
            int temp = 0;
            Receipt receipt = info.GetReceiptByReceiptID(ReceiptID).SingleOrDefault();
            IEnumerable<ReceiptDetail> details = info.GetReceiptDetailByReceiptID(ReceiptID);
            int n = details.Count();
            // Đếm sl receiptdetail cancel
            foreach(ReceiptDetail item in details)
            {
                if(item.Status == "Cancel")
                {
                    temp++;
                }
            }
            // Nếu sl cancel = tổng sl => status = cancel
            if(temp == n)
            {
                info.UpdateReceipt(ReceiptID, "Cancel");
            }
            else // nếu sl cancel < tổng sl 
            {
                info.UpdateReceipt(ReceiptID, Status1);
                foreach(ReceiptDetail item in details)
                {
                    if(item.Status != "Cancel")
                    {
                        info.UpdateReceiptDetail(item.ReceiptDetailID, Status1);
                    }
                }
            }
            return RedirectToAction("GetAllReceiptWaitToConfirm");
        }
        //nhấn nút không duyệt phiếu nhập
        [Route("CancelAll")]
        public ActionResult CancelAll(int ReceiptID)
        {
            string Status = "Cancel";
            //IEnumerable<Receipt> receipt = info.GetReceiptByReceiptID(ReceiptID);
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
            Receipt receipt = info.GetReceiptByReceiptID(receiptDetail.ReceiptID).SingleOrDefault();
            IEnumerable<ReceiptDetail> details = info.GetReceiptDetailByReceiptID(receipt.ReceiptID);
            int n = details.Count();
            int temp = 0; 
            foreach(ReceiptDetail detail in details)
            {
                if (detail.Status == "Confirm" || detail.Status == "Cancel")
                    temp++;
            }
            if(temp == n) // status cuối cùng trong phiếu nhập
            {
                info.UpdateReceipt(receipt.ReceiptID, "Confirm");
                return RedirectToAction("GetAllReceiptWaitToConfirm");
            }
            else
            {
                return RedirectToAction("GetReceiptDetailByReceiptID", new { ReceiptID = receiptDetail.ReceiptID });
            }
        }
        //nhấn nút không duyệt chi tiết phiếu nhập
        [Route("Cancel")]
        public ActionResult Cancel(int ReceiptDetailID)
        {
            ReceiptDetail receiptDetail = info.GetReceiptDetailByReceiptDetailID(ReceiptDetailID);
            Receipt receipt = info.GetReceiptByReceiptID(receiptDetail.ReceiptID).SingleOrDefault();
            IEnumerable<ReceiptDetail> details = info.GetReceiptDetailByReceiptID(receipt.ReceiptID);
            info.UpdateReceiptDetail(ReceiptDetailID, "Cancel");
            int temp1 = 0, temp2 = 0; // số status confirm, cancel
            int n = details.Count();
            foreach(ReceiptDetail detail in details)
            {
                if (detail.Status == "Confirm") temp1++;
                else if (detail.Status == "Cancel") temp2++;
            }
            if((temp1 + temp2) == n) { // status cuối cùng
                if(temp1 != 0) // có status confirm
                {
                    info.UpdateReceipt(receipt.ReceiptID, "Confirm");
                }
                return RedirectToAction("GetAllReceiptWaitToConfirm");
            }
            else
            {
                return RedirectToAction("GetReceiptDetailByReceiptID", new { ReceiptID = receiptDetail.ReceiptID });
            }
        }
        [Route("GetAllReceipt")]
        public ActionResult GetAllReceipt()
        {
            IEnumerable<Receipt> receipt = info.GetAllReceipt();
            return View(receipt);
        }

        [Route("ReceiptDetail")]
        public ActionResult ReceiptDetail(int ReceiptID)
        {
            IEnumerable<ReceiptDetail> details = info.GetReceiptDetailByReceiptID(ReceiptID);
            return View(details);
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
            ConvertAccount c = new ConvertAccount();
             
            ViewData["EmployeeID"] = EmployeeID;
            ViewData["EmployeeName"] = em.Name;
            ViewData["PositionDisplay"] = c.ConvertAccType(acc.AccType) + " " + c.ConvertPosition(acc.Position);
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
            IEnumerable<Employee> employees = info.GetAllEmployee();
            ViewData["employees"] = employees;
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
                sqlBulk.ColumnMappings.Add("Mã nhân viên", "EmployeeID");
                sqlBulk.ColumnMappings.Add("Tên nhân viên", "EmployeeName");
                sqlBulk.ColumnMappings.Add("Lương cơ bản", "BasicSalary");
                sqlBulk.ColumnMappings.Add("Số ngày làm việc", "WorkDay");
                sqlBulk.ColumnMappings.Add("Thưởng", "Bonus");
                sqlBulk.ColumnMappings.Add("Phạt", "Penalty");
                sqlBulk.ColumnMappings.Add("Tổng", "Total");
                sqlBulk.ColumnMappings.Add("Ghi chú", "Desc");
                sqlBulk.ColumnMappings.Add("Đơn vị tiền", "Currency");
                sqlBulk.ColumnMappings.Add("Ngày tạo", "AddedOn");
                sqlBulk.WriteToServer(dReader);
                excelConnection.Close();
                TempData["ResultImportExcel"] = "Nhập thành công";
            }
            return RedirectToAction("ImportExcelFileCreatePayrollForAllEmployee");
        }
        DataSet GetRecordsFromDatabase()
        {
            DataSet dataSet = new DataSet();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["CoffeeShopDBConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select distinct e.EmployeeID, e.Name, s.UnitPrice, p.WorkDay, p.Bonus, p.Penalty, p.[Total], p.[Desc], p.Currency, p.AddedOn FROM((Employee as e left join BasicSalary as bs on e.EmployeeID = bs.EmployeeID) left join Salary as s on bs.SalaryID = s.SalaryID) left join Payroll as p on p.EmployeeID = e.EmployeeID";
            cmd.Connection = conn;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dataSet);

            return dataSet;
        }
        //xuất danh sách nhân viên ra file excel
        [Route("ExportExcel")]
        public ActionResult ExportExcel()
        {
            DataSet dataSet = GetRecordsFromDatabase();
            var excelApp = new Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();

            Worksheet wookSheet = (Worksheet)excelApp.ActiveSheet;
            wookSheet.Name = "Info";

            wookSheet.EnableSelection = Microsoft.Office.Interop.Excel.XlEnableSelection.xlNoSelection;

            var columnName = dataSet.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            
            int i = 0;
            foreach(var col in columnName)
            {
                i++;
                if (col == "Name")
                    wookSheet.Cells[1, i] = "Tên nhân viên";
                else if (col == "UnitPrice")
                    wookSheet.Cells[1, i] = "Lương cơ bản";
                else if (col == "EmployeeID")
                    wookSheet.Cells[1, i] = "Mã nhân viên";
                else if (col == "AddedOn")
                    wookSheet.Cells[1, i] = "Ngày tạo";
                else if (col == "Bonus")
                    wookSheet.Cells[1, i] = "Thưởng";
                else if (col == "Penalty")
                    wookSheet.Cells[1, i] = "Phạt";
                else if (col == "Total")
                    wookSheet.Cells[1, i] = "Tổng";
                else if (col == "Desc")
                    wookSheet.Cells[1, i] = "Ghi chú";
                else if (col == "WorkDay")
                    wookSheet.Cells[1, i] = "Số ngày làm việc";
                else if (col == "Currency")
                    wookSheet.Cells[1, i] = "Đơn vị tiền";
            }
            int j;
            for (i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                int n = dataSet.Tables[0].Columns.Count;
                for (j = 0; j < n; j++)
                {
                    if (j > 2)
                    {
                        wookSheet.Cells[i + 2, j + 1] = "";
                    }
                    //else if (j == n - 1)
                    //{
                    //    var dtm = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    //    wookSheet.Cells[i + 2, j + 1] = dtm;
                    //}
                    else
                    {
                        wookSheet.Cells[i + 2, j + 1] = Convert.ToString(dataSet.Tables[0].Rows[i][j]);
                    }
                    
                }
            }
            wookSheet.SaveAs("E:\\" + wookSheet.Name);
            return RedirectToAction("ImportExcelFileCreatePayrollForAllEmployee");
        }

        [Route("DetailPayRoll")]
        public ActionResult DetailPayRoll(int EmployeeID)
        {
            IEnumerable<Payroll> payroll = info.GetAllAddedOnOfPayroll();
            List<SelectListItem> listAddedOnPayroll = new List<SelectListItem>();
            foreach (var item in payroll.DistinctBy(p => p.AddedOn))
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.AddedOn.ToString();
                listAddedOnPayroll.Add(select);
            }
            ViewData["listAddedOnPayroll"] = listAddedOnPayroll;
            ViewData["Name"] = info.GetInfoEmployeeByEmID(EmployeeID).Name;
            ViewData["EmID"] = info.GetInfoEmployeeByEmID(EmployeeID).EmployeeID;
            return View();
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