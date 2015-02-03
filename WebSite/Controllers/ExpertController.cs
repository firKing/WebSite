using System;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ExpertController : Controller
    {
        // GET: ExpertHome
        //专家个人中心.基本信息
        public ActionResult Home()
        {
            return Info();
        }

        private bool CheckSession()
        {
            return Utility.CheckSession(UserType.Expert, Session);
        }

        private int GetSumPage<T, Tkey>(double count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            return (int)Math.Ceiling(Utility.GetSumCount(whereSelector, keySelector) / count);
        }

        public ActionResult Detail(int id)
        {
            var element = Utility.GetList<expert>(x => x.expertId == id).SingleOrDefault();
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

        private ActionResult Info()
        {
            if (CheckSession())
            {
                var sessionId = Convert.ToInt32(Session["user_id"]);
                var query = Utility.GetList<expert>(x => x.expertId == sessionId);
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        //企业邀请 列表
        public ActionResult InvitationList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;
                var sessionId = Convert.ToInt32(Session["user_id"]);
                ViewBag.list = Utility.GetList<invitation, int>(page, count, x => x.expertId == sessionId, x => x.invitationId);
                ViewBag.sumPage = GetSumPage<invitation, int>(count, x => x.expertId == sessionId, x => x.expertId);
                ViewBag.pageNum = page;
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        //我发布的审核意见列表
        public ActionResult AuditList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;

                var sessionId = Convert.ToInt32(Session["user_id"]);
                var list = Utility.GetList<audit, int>(page, count, x => x.expertId == sessionId, x => x.auditId);
                ViewBag.list = list;
                ViewBag.sumPage = GetSumPage<audit, int>(count, x => x.expertId == sessionId, x => x.expertId);
                ViewBag.pageNum = page;
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}