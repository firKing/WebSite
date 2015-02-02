using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
            var result = GetList<bid>(x => x.bidId == bidId).SingleOrDefault();
            Assert(result != null);
            new SingleTableModule<bid>().Delete(result);
        }

        private String GetFileNameByPath(String path)
        {
            return  System.IO.Path.GetFileName(path);
        }
        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> whereSelector) where T : class
        {
            return Utility.GetList<T>(whereSelector);
        }
        private Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return Utility.CreateRecord(record);
        }
        // GET:
        public ActionResult Detail(int id)
        {
            var element = GetList<bid>(x=>x.bidId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.fileName = GetFileNameByPath(element.bid_content);
                var details = new Pair<bid, List<audit>>
                    (element,
                    GetList<audit>(x => x.bidId == id).ToList());
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
            var purchaseResult = GetList<purchase>(x => x.purchaseId == purchaseId).SingleOrDefault();
            if (CheckVendorSession()&& purchaseResult!=null)
            {
                info.purchase = purchaseResult;

                return View(info);
            }
            Assert(Request.UrlReferrer!=null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        private Pair<bool, bidder> CreateBidder(int tenderId, UserType type)
        {
            return Utility.CreateBidder(tenderId, type);
        }

        private String UploadFileGetUrl(bid info)
        {
            return Utility.UploadFileGetUrl(info, Request);
        }

        [HttpPost]
        public ActionResult Create(bid info)
        {
            //只有Vendor走这里
            if (CheckVendorSession())
            {
                Assert((UserType)Session["user_type"] == UserType.Vendor);
                if (ModelState.IsValid)
                {
                    var bidderResult = CreateBidder((Int32)Session["user_id"], UserType.Vendor);
                    info.bidderId = bidderResult.second.bidderId;
                    info.bid_content = UploadFileGetUrl(info);
                    info.bid_time = DateTime.Now;
                    var getIter = GetList<purchase>(x => x.purchaseId == info.purchaseId).SingleOrDefault();
                    Assert(getIter != null);
                    info.purchase = getIter;
                    var result = CreateRecord<bid>(info);

                    return RedirectToAction("Detail", new { id = result.second.bidId });
                }
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult CreateAudit(audit info)
        {
            if (ModelState.IsValid&& CheckExpertSession())
            {
                var expertId = (Int32) Session["user_id"];
                info.expertId = expertId;
                CreateRecord<audit>(info);
            }
            return RedirectToAction("Detail", new { id = info.bidId });
        }
    }
}