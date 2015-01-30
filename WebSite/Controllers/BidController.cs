﻿using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Web;
namespace WebSite.Controllers
{
    public class BidController : Controller
    {
        private SingleTableModule<bid> db = new SingleTableModule<bid>();
         

        // GET: 
        public ActionResult Detail(int id)
        {
            SingleTableModule<audit> dbAudit = new SingleTableModule<audit>();
            var element = Info(id).SingleOrDefault();
            if (element != null)
            {
                return View(new Pair<bid,IQueryable<audit>>
                    (element, 
                    dbAudit.FindInfo(x => x.bidId == id)));
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        //获取标书详情
        private IQueryable<bid> Info(int id)
        {
            return db.FindInfo(x => x.bidderId == id);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var record = new bid();

            return View(record);
        }
        private Pair<bool,bidder> CreateBidder(int tenderId, UserType type)
        {
            return Utility.CreateBidder(tenderId, type);
        }
        [HttpPost]
        public ActionResult Create(bid info)
        {
            //只有Vendor走这里
            Assert((UserType)Session["user_type"] == UserType.Vendor);
            if (ModelState.IsValid)
            {
                var bidderResult = CreateBidder((Int32)Session["user_id"], UserType.Vendor);
                info.bidderId = bidderResult.second.bidderId;
                var result = db.Create(info);
                return RedirectToAction("Detail", new { id = result.second.bidId });
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        [HttpPost]
        public ActionResult CreateAudit(audit info)
        {
            if (ModelState.IsValid)
            {
                SingleTableModule<audit> dbAudit = new SingleTableModule<audit>();
                var result = dbAudit.Create(info);
                return RedirectToAction("Detail", new { id = info.bidId });
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}