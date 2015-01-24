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
            return Index();
        }
        private bool CheckSession()
        {
            return new Utility().CheckSession(UserType.Expert, Session);
        }
        private object GetList<T>(Func<T, int> expression) where T : class
        {
            return new Utility().GetList<invitation>(x => x.expertId, Session);
        }
        public ActionResult Info()
        {
            if (CheckSession())
            {
                var query =(IQueryable<invitation>) new Utility().GetList<invitation>(x => x.expertId,Session);
                var result = query.SingleOrDefault();
                Assert(result != null);
                return View(result);
            }
            return RedirectToAction("Index", "Index");
        }
        //企业邀请 列表
        public ActionResult InvitationList()
        {
            if (CheckSession())
            {
                return View(new Utility().GetList<invitation>(x => x.expertId,Session));
            }
            return RedirectToAction("Index", "Index");
            
        }
        //我发布的审核意见列表 
        public ActionResult AuditList()
        {
            if (CheckSession())
            {
                return View(new Utility().GetList<audit>(x => x.expertId,Session));
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