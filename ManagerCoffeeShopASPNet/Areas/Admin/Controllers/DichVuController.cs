using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerCoffeeShopASPNet.Areas.Admin.Models;
using ManagerCoffeeShopASPNet.Information;

namespace ManagerCoffeeShopASPNet.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("dichvu")]
    public class DichVuController : Controller
    {
        private InformationDichVu info = new InformationDichVu();
        private InformationWareHouse infoWarehouse = new InformationWareHouse();
        // GET: Admin/DichVu
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPromotion")]
        public ActionResult GetAllPromotion(string search)
        {
            IEnumerable<Promotion> promotion = info.GetAllPromotion();
            List<Promotion> list = promotion.ToList();
            List<Promotion> temp = new List<Promotion>();
            if (!String.IsNullOrEmpty(search))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name.ToLower().Contains(search.ToLower()))
                    {
                        temp.Add(list[i]);
                    }
                }
                promotion = (IEnumerable<Promotion>)temp;
            }
            return View(promotion);
        }

        [Route("GetFormAddNewPromotion")]
        public ActionResult GetFormAddNewPromotion()
        {
            List<SelectListItem> listTypePromotion = new List<SelectListItem>();
            SelectListItem DiscountRate = new SelectListItem();
            DiscountRate.Text = "Giảm theo phần trăm tổng tiền";
            DiscountRate.Value = "DiscountRate";
            SelectListItem DirectDiscountMoney = new SelectListItem();
            DirectDiscountMoney.Text = "Giảm trực tiếp số tiền trên hóa đơn";
            listTypePromotion.Add(DiscountRate);
            listTypePromotion.Add(DirectDiscountMoney);
            ViewData["listTypePromotion"] = listTypePromotion;

            return View();
        }
        [Route("CreatePromotion")]
        public ActionResult CreatePromotion(string Name, string Desc, string dateTimeFrom, string dateTimeTo, string TypePromotion, float Discount, float MinOrderTotalAmount)
        {
            DateTime StartDate = Convert.ToDateTime(dateTimeFrom);
            DateTime EndDate = Convert.ToDateTime(dateTimeTo);
            bool result = info.InsertPromotion(Name, Desc, StartDate, EndDate, TypePromotion, Discount, MinOrderTotalAmount);
            return RedirectToAction("GetAllPromotion", "DichVu");
        }
        [Route("EditPromotion")]
        public ActionResult EditPromotion(int PromotionID)
        {
            List<SelectListItem> listTypePromotion = new List<SelectListItem>();
            SelectListItem DiscountRate = new SelectListItem();
            DiscountRate.Text = "Giảm theo phần trăm tổng tiền";
            DiscountRate.Value = "DiscountRate";
            SelectListItem DirectDiscountMoney = new SelectListItem();
            DirectDiscountMoney.Text = "Giảm trực tiếp số tiền trên hóa đơn";
            listTypePromotion.Add(DiscountRate);
            listTypePromotion.Add(DirectDiscountMoney);
            ViewData["listTypePromotion"] = listTypePromotion;
            Promotion p = info.GetPromotionByID(PromotionID);
            double? Discount = 0;
            Discount = p.DirectDiscountMoney;
            if (p.DiscountRate != null) Discount = p.DiscountRate;
            if (p.DirectDiscountMoney != null) Discount = p.DirectDiscountMoney;
            ViewData["Discount"] = Discount;
            return View(p);
        }
        [Route("DoEditPromotion")]
        public ActionResult DoEditPromotion(int PromotionID, string Name, string Desc, DateTime StartDate, DateTime EndDate, string TypePromotion, float Discount, float MinOrderTotalAmount)
        {
            Promotion p = new Promotion();
            p.PromotionID = PromotionID;
            p.Name = Name;
            p.Desc = Desc;
            p.StartDate = StartDate;
            p.EndDate = EndDate;
            if(TypePromotion == "DiscountRate")
            {
                p.DiscountRate = Discount;
            }
            else
            {
                p.DirectDiscountMoney = Discount;
            }
            bool result = info.EditPromotion(p);
            return RedirectToAction("GetAllPromotion", "DichVu");
        }

        [Route("GetAllGift")]
        public ActionResult GetAllGift(string search)
        {
            IEnumerable<Gift> gift = info.GetAllGift();
            List<Gift> list = gift.ToList();
            List<Gift> temp = new List<Gift>();
            if (!String.IsNullOrEmpty(search))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name.ToLower().Contains(search.ToLower()))
                    {
                        temp.Add(list[i]);
                    }
                }
                gift = (IEnumerable<Gift>)temp;
            }
            return View(gift);
        }

        [Route("GetFormAddNewGift")]
        public ActionResult GetFormAddNewGift()
        {
            List<SelectListItem> listSupplier = new List<SelectListItem>();
            IEnumerable<Supplier> suppliers = infoWarehouse.GetAllSupplier();
            foreach(var item in suppliers)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SupplierID.ToString();
                select.Text = item.Name.ToString();
                listSupplier.Add(select);
            }
            ViewData["listSupplier"] = listSupplier;
            return View();
        }
        [Route("CreateGift")]
        public ActionResult CreateGift(int SupplierID, string Name, float UnitPrice, string Currency, string Desc)
        {
            bool result = info.InsertGift(SupplierID, Name, UnitPrice, Currency, Desc);
            return RedirectToAction("GetAllGift", "DichVu");
        }
        [Route("EditGift")]
        public ActionResult EditGift(int GiftID)
        {
            Gift gift = info.GetGiftByID(GiftID);
            List<SelectListItem> listSupplier = new List<SelectListItem>();
            IEnumerable<Supplier> suppliers = infoWarehouse.GetAllSupplier();
            foreach (var item in suppliers)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SupplierID.ToString();
                select.Text = item.Name.ToString();
                listSupplier.Add(select);
            }
            ViewData["listSupplier"] = listSupplier;
            return View(gift);
        }
        [Route("DoEditGift")]
        public ActionResult DoEditGift(int GiftID, int SupplierID, string Name, float UnitPrice, string Currency, string Desc)
        {
            Gift gift = new Gift();
            gift.GiftID = GiftID;
            gift.SupplierID = SupplierID;
            gift.Name = Name;
            gift.UnitPrice = UnitPrice;
            gift.Currency = Currency;
            gift.Desc = Desc;
            bool result = info.EditGift(gift);
            return RedirectToAction("GetAllGift", "DichVu");
        }
        [Route("DeleteGift")]
        public ActionResult DeleteGift(int GiftID)
        {
            IEnumerable<Gift> gift = info.GetGiftByGiftID(GiftID);
            ViewData["GiftID"] = GiftID;
            return View(gift);
        }
        [Route("DoDeleteGift")]
        public ActionResult DoDeleteGift(int GiftID)
        {
            info.DeleteGift(GiftID);
            return RedirectToAction("GetAllGift", "DichVu");
        }
    }
}