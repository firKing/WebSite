using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class PurchaseController : Controller
    {
        public class IndexStruct
        {
            public String name;
            public Pair<string, int> time;
            public String content;
            public String image;
            public int detailId;
            public int id;
        };
        // GET: Purchase

        // GET:
        public ActionResult Detail(int id)
        {
            var element = GetList<purchase>(x => x.purchaseId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.detail = element;
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        //获取采购信息详情页
        //private IQueryable<purchase> Info(int purchaseId)
        //{
        //    return db.FindInfo(x => x.purchaseId == purchaseId);
        //}

        [HttpGet]
        public ActionResult Create()
        {
            if (Utility.CheckSession(UserType.Company, Session))
            {
                ViewBag.companyName =
               Utility.GetSingleTableRecord<company>(x => x.companyId == (Int32)Session["user_id"]).user.user_name;
                return View(new purchase());
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        private void CreateInvitation(int purchaseId, String invitationContent, List<String> expertNameList)
        {
            //Assert ExpertName Exsit


            Assert(expertNameList
                .Select(x =>
                GetList<expert>(y =>
                    y.user.user_name == x &&
                    y.user.user_type == UserType.Expert.ToString())
                    .SingleOrDefault() != null)
                    .Aggregate((x, y) =>
                        x ==
                        y == true));

            var expertIdList = expertNameList
                .Select(x =>
                    GetList<expert>(y =>
                    y.user.user_name == x &&
                    y.user.user_type == UserType.Expert.ToString())
                .SingleOrDefault().expertId).ToList();
            foreach (var iter in expertIdList)
            {
                var expertId = iter;
                CreateRecord<invitation>(new invitation
                {
                    invitation_content = invitationContent,
                    purchaseId = purchaseId,
                    expertId = expertId,
                    invitation_time = DateTime.Now,
                  //  expert = Utility.GetSingleTableRecord<expert>(x => x.expertId == expertId),
                  //  purchase = Utility.GetSingleTableRecord<purchase>(x => x.purchaseId == purchaseId),
                });
            }
        }

        [HttpPost]
        public ActionResult Create(purchase info, String invitees, String invitationContent)
        {
            
            if (ModelState.IsValid&&Utility.CheckSession(UserType.Company,Session))
            {
                info.companyId = (Int32)Session["user_id"];
                info.purchase_time = DateTime.Now;
                var result = CreateRecord<purchase>(info);
                if (result.first)
                {
                    var inviteesList = invitees.Split(',').ToList();
                    CreateInvitation(result.second.purchaseId, invitationContent, inviteesList);
                    return RedirectToAction("Detail", new { id = result.second.purchaseId });
                }
            }
            return RedirectToAction("Home", "Company");
        }

        private IQueryable<T> GetList<T, TKey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return Utility.GetList(page, count, whereSelector, keySelector);
        }
        private Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return Utility.CreateRecord(record);
        }
        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> whereSelector) where T : class
        {
            return Utility.GetList<T>(whereSelector);
        }
        private int GetSumPage<T, TKey>(double count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return (int)Math.Ceiling(Utility.GetSumCount(whereSelector, keySelector) / count);
        }


        private String GetPurchaseTitle(int purchaseId)
        {
            var result = GetList<purchase>(x => x.purchaseId == purchaseId).SingleOrDefault();
            Assert(result != null);
            return result.purchase_title;
        }

        public ActionResult BidList(int purchaseId, int page)
        {
            const int count = 5;
            ViewBag.list = GetList<bid, int>(page, count, x => x.purchaseId == purchaseId, x => x.bidId).ToList().Select(x=>new IndexStruct {
                    detailId = x.bidId,
                    name = x.bid_title,
                    content = x.bid_introduction,
                    time = new Pair<string, int>(Utility.DateTimeToString(x.bid_time), 0)});
            ViewBag.pageSum = GetSumPage<bid, int>(count,x => x.purchaseId == purchaseId, x => x.bidId) ;
            ViewBag.pageNum = page;
            
            ViewBag.detailActionName = "Bid";
            ViewBag.PurchaseTitle = GetPurchaseTitle(purchaseId);
            ViewBag.bigtitle = ViewBag.PurchaseTitle + "的标书列表";
            return View("~/Views/Shared/list.cshtml");
        }

        //ajax 返回字符串 "ture" "false"
        [HttpPost]
        public ActionResult PurchaseHitBid(int purchaseId, int bidId)
        {
            var result = GetList<purchase>(x => x.purchaseId == purchaseId).SingleOrDefault();
            Assert(result != null);
            result.hitId = bidId;
           var sign = new SingleTableModule<purchase>().Edit(result);
           return Json(sign, JsonRequestBehavior.AllowGet);
        }
    }
}