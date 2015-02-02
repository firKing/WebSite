using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
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
                ViewBag.details = new Pair<bid, List<audit>>
                    (element,
                    GetList<audit>(x => x.bidId == id).ToList());
                
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        //获取标书详情
        public ActionResult Create()
        {
            if (CheckVendorSession())
            {
                return View();
            }
            Assert(Request.UrlReferrer!=null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        private Pair<bool, bidder> CreateBidder(int tenderId, UserType type)
        {
            return Utility.CreateBidder(tenderId, type);
        }

        private List<String> UploadFileGetUrl(bid info)
        {
            var result = new List<String>();
            foreach (string upload in Request.Files)
            {
                Assert(upload == "bid_content");
                Assert(Request.Files.Count == 1);
                Assert(Request.Files[upload] != null);
                string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                Assert(filename != null);
                path = Path.Combine(path, filename);
                Request.Files[upload].SaveAs(path);
                //info.bid_content = path;
                result.Add(path);
            }
            Assert(result.Count()==1);
            return result;
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
                    info.bid_content = UploadFileGetUrl(info).SingleOrDefault();

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
                var result = CreateRecord<audit>(info);
            }
            return RedirectToAction("Detail", new { id = info.bidId });
        }
    }
}