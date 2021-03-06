﻿using ManagerCoffeeShopASPNet.Areas.Admin.Models;
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
        private InformationDichVu infoDV = new InformationDichVu();
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
        public ActionResult DoEditIngredient(int IngreID, int SupplierID, string Name, double Amount, double AmountMin, string Unit, double UnitPrice, string Currency)
        {
            bool result = info.EditIngredient(IngreID, SupplierID, Name, Amount, AmountMin, Unit, UnitPrice, Currency);
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
        public ActionResult DoCreateIngredient(int SupplierID, string Name, double Amount, double AmountMin, string Unit, double UnitPrice, string Currency)
        {
            
            bool result = info.InsertIngredient(SupplierID, Name, Amount, AmountMin, Unit, UnitPrice, Currency);
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
            if (Name == null || Address == null || Phone == null || Name == "" || Address == "" || Phone == "")
            {
                TempData["ErrorInfo"] = "Hãy điền đầy đủ thông tin";
            }
            else
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
            //int ReceiptDetailID = receiptDetails.Single().ReceiptDetailID;
            ViewData["ReceiptID"] = ReceiptID;
            Receipt receipt = info.GetReceiptByID(ReceiptID);
            ViewData["StatusReceipt"] = receipt.Status;
            return View(receiptDetails);
        }
        //nhấn nút nhập xong phiếu nhập và cập nhật lại số lượng vào kho (Ingredients)
        [Route("ClosedReceipt")]
        public ActionResult ClosedReceipt(int ReceiptID)
        {
            string status = "Closed";
            
            //ReceiptDetail detail = info.GetReceiptDetailByReceiptDetailID(ReceiptDetailID);
            IEnumerable<ReceiptDetail> details = info.GetAllReceiptDetailByReceiptID(ReceiptID);
            foreach (var item in details)
            {
                info.UpdateReceiptDetail(item.ReceiptDetailID, status);
                //info.UpdateReceiptDetailByReceiptID(ReceiptID, status);
                if (item.IngreID != null)
                {
                    int IngreID = item.IngreID.GetValueOrDefault();
                    Ingredient ingre = info.GetIngredientByIngreID(IngreID);
                    double AmountOfReceiptDetail = item.Amount.GetValueOrDefault();
                    double AmountNew = AmountOfReceiptDetail + ingre.Amount;
                    info.UpdateAmountIngredient(IngreID, AmountNew);
                }
                //if (item.GiftID != null)
                //{
                //    int GiftID = item.GiftID.GetValueOrDefault();
                //    Gift gift = infoDV.GetGiftByID(GiftID);
                //    double AmountOfReceiptDetail = item.Amount.GetValueOrDefault();
                //    double AmountNew=AmountOfReceiptDetail+gift
                //}
            }

            bool result = info.UpdateReceipt(ReceiptID, status);
            if (result)
            {
                TempData["message"] = "Lưu thành công";
            }
            else
            {
                TempData["error"] = "Lưu thất bại";
            }
            return RedirectToAction("DetailReceipt", "Warehouse", new { ReceiptID = ReceiptID});
        }
        [Route("CreateReceipt")]
        public ActionResult CreateReceipt(string json)
        {
            if (json == null)
            {
                IEnumerable<Ingredient> ingres = info.GetAllIngredient();
                IEnumerable<Gift> gifts = infoDV.GetAllGift();
                IEnumerable<Supplier> suppliers = info.GetAllSupplier();
                List<SelectListItem> sups = new List<SelectListItem>();
                foreach (Supplier item in suppliers)
                {
                    SelectListItem select = new SelectListItem();
                    select.Text = item.Name;
                    select.Value = item.SupplierID.ToString();
                    sups.Add(select);
                }
                IEnumerable<Ingredient> ingreEffete = info.GetAllIngredientEffete();
                ViewData["test"] = ingreEffete;
                ViewData["gifts"] = gifts;
                ViewData["sups"] = sups;
                ViewData["ingres"] = ingres;
                return View();
            }
            else
            {
                ListIngreEffete ingreModel = JsonConvert.DeserializeObject<ListIngreEffete>(json);
                IEnumerable<Ingredient> ingres = info.GetAllIngredient();
                IEnumerable<Gift> gifts = infoDV.GetAllGift();
                IEnumerable<Supplier> suppliers = info.GetAllSupplier();
                List<SelectListItem> sups = new List<SelectListItem>();
                foreach (Supplier item in suppliers)
                {
                    SelectListItem select = new SelectListItem();
                    select.Text = item.Name;
                    select.Value = item.SupplierID.ToString();
                    sups.Add(select);
                }
                ViewData["gifts"] = gifts;
                ViewData["sups"] = sups;
                ViewData["ingres"] = ingres;
                List<IngredientEffete> details = ingreModel.IngredientEffete;
                foreach(IngredientEffete item in details)
                {
                    int IngreID = item.IngreID;
                    string Name = item.Name;
                    double UnitPrice = item.UnitPrice;
                    
                }
                //return View();
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
        //[Route("")]
        [Route("DoCreateReceipt")]
        public ActionResult DoCreateReceipt(string json)
        {
            ReceiptModel receiptModel = JsonConvert.DeserializeObject<ReceiptModel>(json);
            List<ReceiptDetailModel> details = receiptModel.ReceiptDetailModel;
            double Total = 0;
            foreach (ReceiptDetailModel detail in details)
            {
                Total = Total + detail.TotalAmount * detail.Quantity + detail.TotalAmount_gift * detail.Quantity_gift;
            }
            info.InsertReceipt(DateTime.Now, Total, "VND", "Waiting");
            int ReceiptID = info.GetLastReceiptID();
            foreach(ReceiptDetailModel detail in details)
            {
                int IngreID = detail.IngreID;
                int GiftID = detail.GiftID;
                if (IngreID != 0)
                {
                    Ingredient ingre = info.GetIngredientByIngreID(IngreID);
                    bool result = info.InsertReceiptDetailMissGiftID(ReceiptID, IngreID, detail.Quantity, ingre.Unit, ingre.UnitPrice, detail.ReferenceDesc, ingre.Currency, "Waiting");
                }
                if (GiftID != 0)
                {
                    Gift gift = infoDV.GetGiftByID(GiftID);
                    bool result = info.InsertReceiptDetailMissIngreID(ReceiptID, GiftID, detail.Quantity_gift, gift.UnitPrice, detail.ReferenceDesc_gift, gift.Currency, "Waiting");
                }
            }
            return Json(new { SupplierID = receiptModel.SupplierID }, JsonRequestBehavior.AllowGet);
        }

        [Route("GetAllIngredientEffete")]
        public ActionResult GetAllIngredientEffete()
        {
            IEnumerable<Ingredient> ingres = info.GetAllIngredientEffete();
            //ViewData["test"] = ingres;
            return RedirectToAction("CreateReceipt", "Warehouse");
        }
    }
}