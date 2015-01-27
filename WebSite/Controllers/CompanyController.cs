using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Linq.Expressions;
namespace WebSite.Controllers
{
    public class CompanyController : Controller
    {
        // GET: CompanyHome
        public ActionResult Home()
        {
            return Info();
        }
        private bool CheckSession()
        {
            return new Utility().CheckSession(UserType.Company, Session);
        }

        private ActionResult Info()
        {
            if (CheckSession()) 
            {
                IQueryable<company> query = (IQueryable<company>)(new Utility().GetList<company>(x => x.companyId, Session));
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
        //获取companyId有关的列表
        private ActionResult GetList<T>(Func<T, int> expression) where T : class
        {
            if (CheckSession())
            {
                return View(new Utility().GetList<T>(expression,Session));
            }
            return RedirectToAction("Index", "Index");
        }
        //发布的采购信息列表 
        public ActionResult PurchaseInfoList()
        {
            return GetList<purchase>(x => x.companyId);
        }
        //发布的新闻列表
        public ActionResult NewsList()
        {
            return GetList<news>(x => x.companyId);
        }
        public ActionResult InvitationList()
        {
            return GetList<invitation>(x => x.purchase.companyId);
        }
    }
}