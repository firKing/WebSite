using System;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ExpertController : Controller
    {
        // GET: ExpertHome
        //专家个人中心.基本信息
        public ActionResult Index()
        {
            return Info();
        }

        public ActionResult Home()
        {
            return Info();
        }

        private bool CheckSession()
        {
            return Utility.CheckSession(UserType.Expert, Session);
        }

        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return Utility.GetList<T>(expression);
        }

        private int GetSumCount<T, TKey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return Utility.GetSumCount(whereSelector, keySelector);
        }

        public ActionResult Detail(int id)
        {
            var db = new SingleTableModule<expert>();

            var element = db.FindInfo(x => x.expertId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.name = element.user.user_name;
                ViewBag.content = element.user.user_introduction;
                return View("~/Views/Shared/detail.cshtml");
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
                var query = GetList<expert>(x => x.expertId == sessionId);
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        //企业邀请 列表
        public ActionResult InvitationList(int page)
        {
            if (CheckSession())
            {
                var sessionId = Convert.ToInt32(Session["user_id"]);
                ViewBag.list = GetList<invitation>(x => x.expertId == sessionId);
                ViewBag.sumPage = GetSumCount<invitation, int>(x => x.expertId == sessionId, x => x.expertId);
                ViewBag.pageNum = page;

                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        //我发布的审核意见列表
        public ActionResult AuditList(int page)
        {
            if (CheckSession())
            {
                var sessionId = Convert.ToInt32(Session["user_id"]);
                ViewBag.list = GetList<audit>(x => x.expertId == sessionId);
                ViewBag.sumPage = GetSumCount<audit, int>(x => x.expertId == sessionId, x => x.expertId);
                ViewBag.pageNum = page;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        ////管理员的专家列表
        //public ActionResult List(int page)
        //{
        //    var table = new SingleTableModule<expert>();
        //    var result = table.FindInfo().Skip(page * 8).Take(8);
        //    return View(result);
        //}
    }
}