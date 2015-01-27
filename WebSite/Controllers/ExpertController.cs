using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;

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
            return new Utility().CheckSession(UserType.Expert, Session);
        }
        private object GetList<T>(Func<T, int> expression) where T : class
        {
            return new Utility().GetList<T>(expression, Session);
        }
        private ActionResult Info()
        {
            if (CheckSession())
            {
                var query =(IQueryable<expert>) GetList<expert>(x => x.expertId);
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
        //企业邀请 列表
        public ActionResult InvitationList()
        {
            if (CheckSession())
            {
                ViewBag.list = GetList<invitation>(x => x.expertId);
                return View();
            }
            return RedirectToAction("Index", "Index");
            
        }
        //我发布的审核意见列表 
        public ActionResult AuditList()
        {
            if (CheckSession())
            {
                return View(GetList<audit>(x => x.expertId));
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