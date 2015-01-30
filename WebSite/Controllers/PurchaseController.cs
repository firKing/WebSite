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
                        x == true &&
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
                    return View("Detail");
                }
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        //[HttpPost]
        //public ActionResult CreateInvitation(invitation info)
        //{
        //    var dbInvitation = new SingleTableModule<invitation>();
        //    var result = dbInvitation.Create(info);
        //    return View("Detail");
        //}
        private IQueryable<T> GetList<T, Tkey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {

            return Utility.GetList(page, count,whereSelector, keySelector);
        }
        public int GetSumCount<T, Tkey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            return Utility.GetSumCount<T, Tkey>(whereSelector, keySelector);
        }
        public ActionResult BidList(int purachseId,int page)
        {
            int count = 5;
            ViewBag.list = GetList<bid, int>(page, count, x => x.purchaseId == purachseId, x => x.bidId);
            ViewBag.sumPage = GetSumCount<bid,int>(x => x.purchaseId == purachseId, x => x.bidId);
            ViewBag.page = page + 1;
            return View();
        }
    }
}