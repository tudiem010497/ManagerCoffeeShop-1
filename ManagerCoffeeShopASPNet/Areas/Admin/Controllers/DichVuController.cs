﻿using System;
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
            return View();
        }
        [Route("CreatePromotion")]
        public ActionResult CreatePromotion(string Name, string Desc, DateTime StartDate, DateTime EndDate)
        {
            bool result = info.InsertPromotion(Name, Desc, StartDate, EndDate);
            return RedirectToAction("GetAllPromotion", "DichVu");
        }
        [Route("EditPromotion")]
        public ActionResult EditPromotion(int PromotionID)
        {
            Promotion p = info.GetPromotionByID(PromotionID);
            return View(p);
        }
        [Route("DoEditPromotion")]
        public ActionResult DoEditPromotion(int PromotionID, string Name, string Desc, DateTime StartDate, DateTime EndDate)
        {
            Promotion p = new Promotion();
            p.PromotionID = PromotionID;
            p.Name = Name;
            p.Desc = Desc;
            p.StartDate = StartDate;
            p.EndDate = EndDate;
            bool result = info.EditPromotion(p);
            return RedirectToAction("GetAllPromotion", "DichVu");
        }
        [Route("GetAllGift")]
        public ActionResult GetAllGift()
        {
            IEnumerable<Gift> gift = info.GetAllGift();
            return View(gift);
        }

        [Route("GetFormAddNewGift")]
        public ActionResult GetFormAddNewGift()
        {
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
            Gift gift = info.GetGiftByID(GiftID);
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