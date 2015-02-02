using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Common.Utility;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class VendorController : Controller
    {
        // GET: VendorHome
        public ActionResult Home()
        {
            return Info();
        }

        public ActionResult Detail(int id)
        {
            var element = GetList<vendor>(x => x.vendorId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.name = element.user.user_name;
                ViewBag.phone = element.user.user_telephone;
                ViewBag.email = element.user.user_mail;
                ViewBag.address = element.user.user_address;
                ViewBag.content = element.user.user_introduction;
                return View("~/Views/Shared/userDetail.cshtml");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Info()
        {
            if (CheckSession())
            {
                Assert(Session["user_id"] != null);

                var sessionId = Convert.ToInt32(Session["user_id"]);

                var query = GetList<vendor>(x => x.vendorId == sessionId).ToList();
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        //加入的团队列表
        public ActionResult AddTeamList(int page)
        {
            if (CheckSession())
            {
                Assert(Session["user_id"] != null);

                const int count = 5;
                var sessionId = Convert.ToInt32(Session["user_id"]);

                ViewBag.pageSum = GetSumCount<member, int>(x => x.vendorId == sessionId, x => x.memberId);
                var pageSum = ViewBag.pageSum;
                ViewBag.pageNum = page;
                ViewBag.troop = GetList<member>(page,count,
                    x =>
                    x.vendorId == sessionId,x=>x.memberId)
                    .ToList().Select(x =>
                    new Pair<team, List<member>>(
                        GetList<team>(
                            y =>
                            y.teamId == x.teamId)
                            .SingleOrDefault(),
                        GetList<member>(
                            y =>
                            y.teamId == x.teamId).ToList()));
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        public ActionResult CreateTeamList(int page)
        {
            if (CheckSession())
            {
                Assert(Session["user_id"] != null);

                var sessionId = Convert.ToInt32(Session["user_id"]);
                const int count = 5;
                ViewBag.pageSum = GetSumCount<team, int>(x => x.createId == sessionId, x => x.teamId);
                ViewBag.pageNum = page;

                ViewBag.teamList = GetList<team>(page,count,x =>
                    x.createId == sessionId,x=>x.teamId).ToList()
                    .Select(x =>
                        new Pair<team, List<member>>(
                            x, GetList<member>(y =>
                                y.teamId == x.teamId).ToList()));
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        public ActionResult PublishBidList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;
                Assert(Session["user_id"] != null);
                var sessionId = Convert.ToInt32(Session["user_id"]);
                var id = GetBidderId(sessionId);
                ViewBag.personal = GetList<bid>(page, count, x => x.bidderId == id,x=>x.bidId).ToList().Select(x=>new Pair<bid, BidUserInfo>(x,GetBidUser(x.bidder)));
                ViewBag.pageSum = GetSumCount<bid, int>(x => x.bidderId == id, x => x.bidId);
                ViewBag.pageNum = page;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        private int GetBidderId(int vendorId)
        {
            var element = GetList<bidder>(x => x.tendererId == vendorId && x.bidder_is_team == false).SingleOrDefault();
            Assert(element != null);
            return element.bidderId;
        }

        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return Utility.GetList<T>(expression);
        }

        private int GetSumCount<T,Tkey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            return Utility.GetSumCount(whereSelector, keySelector);
        }

        private IQueryable<T> GetList<T>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, int>> keySelector) where T : class
        {
            return Utility.GetList<T, int>(page, count, whereSelector,keySelector);
        }
        private bool CheckSession()
        {
            return Utility.CheckSession(UserType.Vendor, Session);
        }

        /*
        M我加入的虚拟团队列表 			pair<model<team>,List<model<member>>> GetAddVirtualTeamList(int vendorId);查memeber表,查team表
	M我创建的虚拟团队列表 			pair<model<team>,List<model<member>>> GetCreatedVirtualTeamList(int vendorId);
memeber team
	M查看发布的投标列表 			List<model<bid>>GetPublishBidList(int vendorId);

        */
    }
}