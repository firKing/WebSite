using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Common.Utility;
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
            var element = Utility.GetList<vendor>(x => x.vendorId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.creator = Utility.GetSingleTableRecord<vendor>(x => x.vendorId == element.vendorId).user.user_name;
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

                var query = Utility.GetList<vendor>(x => x.vendorId == sessionId).ToList();
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        //加入的团队列表
        public ActionResult AddTeamList(int page)
        {
            if (CheckSession())
            {
                Assert(Session["user_id"] != null);

                const int count = 5;
                var sessionId = Convert.ToInt32(Session["user_id"]);

                ViewBag.pageSum = GetSumPage<member, int>(count, x => x.vendorId == sessionId, x => x.memberId);
                var pageSum = ViewBag.pageSum;
                ViewBag.pageNum = page;
                ViewBag.troop = Utility.GetList<member, int>(page-1, count,
                    x =>
                    x.vendorId == sessionId, x => x.memberId)
                    .ToList().Select(x =>
                    new Pair<team, List<member>>(
                        Utility.GetList<team>(
                            y =>
                            y.teamId == x.teamId)
                            .SingleOrDefault(),
                        Utility.GetList<member>(
                            y =>
                            y.teamId == x.teamId).ToList()));
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult CreateTeamList(int page)
        {
            if (CheckSession())
            {
                Assert(Session["user_id"] != null);

                var sessionId = Convert.ToInt32(Session["user_id"]);
                const int count = 5;
                ViewBag.pageSum = GetSumPage<team, int>(count, x => x.createId == sessionId, x => x.teamId);
                ViewBag.pageNum = page;

                ViewBag.teamList = Utility.GetList<team, int>(page-1, count, x =>
                       x.createId == sessionId, x => x.teamId).ToList()
                    .Select(x =>
                        new Pair<team, List<member>>(
                            x, Utility.GetList<member>(y =>
                                y.teamId == x.teamId).ToList()));
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult PublishBidList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;
                Assert(Session["user_id"] != null);
                var sessionId = Convert.ToInt32(Session["user_id"]);
                var id = GetBidderId(sessionId);
                var personal = Utility.GetList<bid, int>(page-1, count, x => x.bidderId == id, x => x.bidId).ToList().Select(x => new Pair<bid, BidUserInfo>(x, GetBidUser(x.bidder)));
                ViewBag.personal = personal;
                var pageSum = GetSumPage<bid, int>(count, x => x.bidderId == id, x => x.bidId);
                ViewBag.pageSum = pageSum;
                ViewBag.pageNum = page;
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        private int GetBidderId(int vendorId)
        {
            var element = Utility.GetList<bidder>(x => x.tendererId == vendorId && x.bidder_is_team == false).SingleOrDefault();
            Assert(element != null);
            return element.bidderId;
        }

        private int GetSumPage<T, Tkey>(double count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            return (int)Math.Ceiling(Utility.GetSumCount(whereSelector, keySelector) / count);
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