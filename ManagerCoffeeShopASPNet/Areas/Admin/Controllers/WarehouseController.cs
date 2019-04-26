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
    [RouteArea("admin")]
    [RoutePrefix("warehouse")]
    public class WarehouseController : Controller
    {
        private InformationWareHouse info = new InformationWareHouse();
        // GET: Admin/Warehouse
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllIngredient")]
        public ActionResult GetAllIngredient()
        {
            IEnumerable<Ingredient> ingres = info.GetAllIngredient();
            return View(ingres);
        }
         [Route("EditIngredient")]
         public ActionResult EditIngredient(int IngreID)
        {
            Ingredient ingre = info.GetIngredientByIngreID(IngreID);
            IEnumerable<Supplier> suppliers = info.GetAllSupplier();
            List<SelectListItem> listSupplier = new List<SelectListItem>();
            foreach(var item in suppliers)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.Name;
                select.Value = item.SupplierID.ToString();
                listSupplier.Add(select);
            }
            ViewData["listSupplier"] = listSupplier;
            return View(ingre);
        }

        [Route("DoEditIngredient")]
        public ActionResult DoEditIngredient(int IngreID, int SupplierID, string Name, double Amount, string Unit, double UnitPrice, string Currency)
        {
            bool result = info.EditIngredient(IngreID, SupplierID, Name, Amount, Unit, UnitPrice, Currency);
            if (result)
            {
                TempData["message"] = "Lưu thành công";
            }
            else
            {
                TempData["error"] = "Lưu thất bại";
            }
            return RedirectToAction("GetAllIngredient", "Warehouse");
        }

        [Route("CreateIngredient")]
        public ActionResult CreateIngredient()
        {
            IEnumerable<Supplier> suppliers = info.GetAllSupplier();
            List<SelectListItem> listSupplier = new List<SelectListItem>();
            foreach (var item in suppliers)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.Name;
                select.Value = item.SupplierID.ToString();
                listSupplier.Add(select);
            }
            ViewData["listSupplier"] = listSupplier;
            return View();
        }

        [Route("DoCreateIngredient")]
        public ActionResult DoCreateIngredient(int SupplierID, string Name, double Amount, string Unit, double UnitPrice, string Currency)
        {
            bool result = info.InsertIngredient(SupplierID, Name, Amount, Unit, UnitPrice, Currency);
            if (result)
            {
                TempData["message"] = "Lưu thành công";
            }
            else
            {
                TempData["error"] = "Lưu thất bại";
            }
            return RedirectToAction("CreateIngredient", "Warehouse");
        }

        [Route("GetAllSupplier")]
        public ActionResult GetAllSupplier()
        {
            IEnumerable<Supplier> suppliers = info.GetAllSupplier();
            return View(suppliers);
        }

        [Route("EditSupplier")]
        public ActionResult EditSupplier(int SupplierID)
        {
            Supplier sup = info.GetSupplierBySupplierID(SupplierID);
            return View(sup);
        }

        [Route("DoEditSupplier")]
        public ActionResult DoEditSupplier(int SupplierID, string Name, string Address, string Phone)
        {
            bool result = info.EditSupplier(SupplierID, Name, Address, Phone);
            if (result)
            {
                TempData["message"] = "Lưu thành công";
            }
            else
            {
                TempData["error"] = "Lưu thất bại";
            }
            return RedirectToAction("GetAllSupplier", "Warehouse");

        }

        [Route("CreateSupplier")]
        public ActionResult CreateSupplier()
        {
            return View();
        }

        [Route("DoCreateSupplier")]
        public ActionResult DoCreateSupplier(string Name, string Address, string Phone)
        {
            bool result = info.InsertSupplier(Name, Address, Phone);
            if (result)
            {
                TempData["message"] = "Lưu thành công";
            }
            else
            {
                TempData["error"] = "Lưu thất bại";
            }
            return RedirectToAction("CreateSupplier", "Warehouse");
        }

        [Route("GetAllReceipt")]
        public ActionResult GetAllReceipt()
        {
            IEnumerable<Receipt> receipts = info.GetAllReceipt();
            return View(receipts);
        }

        [Route("DetailReceipt")]
        public ActionResult DetailReceipt(int ReceiptID)
        {
            IEnumerable<ReceiptDetail> receiptDetails = info.GetAllReceiptDetailByReceiptID(ReceiptID);
            return View(receiptDetails);
        }

        [Route("CreateReceipt")]
        public ActionResult CreateReceipt()
        {
            IEnumerable<Ingredient> ingres = info.GetAllIngredient();
            IEnumerable<Supplier> suppliers = info.GetAllSupplier();
            List<SelectListItem> sups = new List<SelectListItem>();
            foreach(Supplier item in suppliers)
            {
                SelectListItem select = new SelectListItem();
                select.Text = item.Name;
                select.Value = item.SupplierID.ToString();
                sups.Add(select);
            }
            ViewData["sups"] = sups;
            ViewData["ingres"] = ingres;
            return View();
        }
        
        [Route("DoCreateReceipt")]
        public ActionResult DoCreateReceipt(string json)
        {
            ReceiptModel receiptModel = JsonConvert.DeserializeObject<ReceiptModel>(json);
            List<ReceiptDetailModel> details = receiptModel.ReceiptDetailModel;
            double TotalAmount = 0;
            foreach(ReceiptDetailModel detail in details)
            {
                TotalAmount = TotalAmount + detail.TotalAmount * detail.Quantity;
            }
            info.InsertReceipt(DateTime.Now, TotalAmount, "VND", "Waiting");
            int ReceiptID = info.GetLastReceiptID();
            foreach(ReceiptDetailModel detail in details)
            {
                int IngreID = detail.IngreID;
                Ingredient ingre = info.GetIngredientByIngreID(IngreID);
                bool result = info.InsertReceiptDetail(ReceiptID, IngreID, detail.TotalAmount, ingre.Unit, ingre.UnitPrice, ingre.Currency, "Waiting");
            }
            return Json(new { SupplierID = receiptModel.SupplierID }, JsonRequestBehavior.AllowGet);
        }
    }
}