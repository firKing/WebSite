using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics.Debug;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Common.Utility;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class BidController : Controller
    {
        private bool CheckVendorSession()
        {
            return Utility.CheckSession(UserType.Vendor, Session);
        }

        private bool CheckExpertSession()
        {
            return Utility.CheckSession(UserType.Expert, Session);
        }


        //ajax
        [HttpPost]
        public void DeleteBid(int bidId)
        {
            var result = Utility.GetSingleTableRecord<bid>(x => x.bidId == bidId);
            Assert(result != null);
            new SingleTableModule<bid>().Delete(result);
        }

        private String GetFileNameByPath(String path)
        {
            return System.IO.Path.GetFileName(path);
        }

        private Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return Utility.CreateRecord(record);
        }

        // GET:
        public ActionResult Detail(int id)
        {
            var element = Utility.GetSingleTableRecord<bid>(x => x.bidId == id);
            if (element != null)
            {
                ViewBag.fileName = GetFileNameByPath(element.bid_content);
                var details = new Pair<bid, List<audit>>
                    (element,
                    Utility.GetList<audit>(x => x.bidId == id).ToList());
                ViewBag.Details = details;
                ViewBag.bidderName = GetBidUser(element.bidder);
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        //获取标书详情
        public ActionResult Create(int purchaseId)
        {
            var info = new bid();
            if (CheckVendorSession())
            {
                info.purchaseId = purchaseId;
                ViewBag.purchaseTitle = Utility.GetSingleTableRecord<purchase>(x => x.purchaseId == purchaseId).purchase_title;
                return View(info);
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        private Pair<bool, bidder> CreateBidder(int tenderId, UserType type)
        {
            return Utility.CreateBidder(tenderId, type);
        }

        [HttpPost]
        public ActionResult Create(bid info)
        {
            //只有Vendor走这里
            if (CheckVendorSession())
            {
                Assert((UserType)Session["user_type"] == UserType.Vendor);
                var bidderResult = CreateBidder((Int32)Session["user_id"], UserType.Vendor);
                if (ModelState.IsValid && bidderResult.first)
                {
                    const String uploadFieldName = "bid_content";
                    Utility.FillBidRecord(info, bidderResult.second, Request,uploadFieldName);
                    var result = new Pair<bool, bid>();
                    result = CreateRecord<bid>(info);
                    return RedirectToAction("Detail", new { id = result.second.bidId });
                }
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult CreateAudit(audit info)
        {
            if (ModelState.IsValid && CheckExpertSession())
            {
                var expertId = (Int32)Session["user_id"];
                Utility.EditRecord<expert>(x => x.expertId == expertId, (x) =>
                {
                    x.expert_accept_count += 1;
                    return x;
                });
                info.expertId = expertId;
                info.audit_time = DateTime.Now;
                CreateRecord<audit>(info);
            }
            return RedirectToAction("Detail", new { id = info.bidId });
        }
    }
}