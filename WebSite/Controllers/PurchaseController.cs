using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Web;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace WebSite.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        private SingleTableModule<purchase> db = new SingleTableModule<purchase>();

        // GET: 
        public ActionResult Detail(int id)
        {
            var element = Info(id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.detail = element;
                return View("~/Views/Purchase/Detail.cshtml");
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        //获取采购信息详情页
        private IQueryable<purchase> Info(int purchaseId)
        {
            return db.FindInfo(x => x.purchaseId == purchaseId);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var record = new purchase();
            return View(record);
        }

        private void CreateInvitation(int purchaseId,String invitationContent,List<String>expertNameList)
        {
            //Assert ExpertName Exsit
            
            var expertTable = new SingleTableModule<expert>();

            Assert(expertNameList
                .Select(x =>
                expertTable.FindInfo(y =>
                    y.user.user_name == x &&
                    y.user.user_type == UserType.Expert.ToString())
                    .SingleOrDefault() != null)
                    .Aggregate((x, y) =>
                        x ==
                        y == true));

            var expertIdList = expertNameList
                .Select(x =>
                expertTable.FindInfo(y =>
                y.user.user_name == x &&
                y.user.user_type == UserType.Expert.ToString())
                .SingleOrDefault().expertId).ToList();
            var invitationTable = new SingleTableModule<invitation>(); 
            foreach (var expertId in expertIdList)
            {
                invitationTable.Create(new invitation
                {
                    invitation_content = invitationContent,
                    purchaseId = purchaseId,
                    expertId = expertId
                });
            }
        }
        [HttpPost]
        public ActionResult Create(purchase info,String invitees,String invitationContent)
        {
            if (ModelState.IsValid)
            {
                var result = db.Create(info);
                if (result.first == true)
                {
                    var invitationTable = new SingleTableModule<invitation>();
                    var inviteesList = invitees.Split(',').ToList();
                    CreateInvitation(result.second.purchaseId, invitationContent, inviteesList);
                    return RedirectToAction("Detail",new { id = result.second.purchaseId });
                }
            }
            return RedirectToAction("Home","Company");
        }

        private IQueryable<T> GetList<T, Tkey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {

            return Utility.GetList(page, count,whereSelector, keySelector);
        }
        public int GetSumCount<T, Tkey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            return Utility.GetSumCount<T, Tkey>(whereSelector, keySelector);
        }
        public ActionResult BidList(int purchaseId,int page)
        {
            const int count = 5;
            ViewBag.list = GetList<bid, int>(page, count, x => x.purchaseId == purchaseId, x => x.bidId);
            ViewBag.sumPage = GetSumCount<bid,int>(x => x.purchaseId == purchaseId, x => x.bidId);
            ViewBag.page = page;
            var result = new SingleTableModule<purchase>().FindInfo(x => x.purchaseId == purchaseId).SingleOrDefault();
            Assert(result != null);
            ViewBag.PurchaseTitle = result.purchase_title;
            return View();
        }

        
        //ajax
        [HttpPost]
        public void PurchaseHitBid(int purchaseId,int bidId)
        {
            var result = db.FindInfo(x => x.purchaseId == purchaseId).SingleOrDefault();
            Assert(result != null);
            result.hitId = bidId;
            db.Edit(result);
        }
    }
}