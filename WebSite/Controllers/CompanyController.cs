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
                IQueryable<user> query = (IQueryable<user>)(new Utility().GetList<user>(x => x.userId, Session));
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
        //获取companyId有关的列表
        private void List<T,Tkey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            ViewBag.list = GetList<T, Tkey>(page, count, whereSelector, keySelector).ToList();
        }
        private IQueryable<T> GetList<T, Tkey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo(whereSelector).OrderByDescending(keySelector).Skip(page * count).Take(count);
            return container;
        }
        //发布的采购信息列表 
        public ActionResult PurchaseInfoList(int page)
        {
            if (CheckSession())
            {
                int count = 5;
                List<purchase, int>(page, count, x => x.companyId.ToString() == Session["user_id"].ToString(), x => x.purchaseId);
                ViewBag.page = page + 1;
                return View();
            }
            return RedirectToAction("Index", "Index");
           
        }
        //发布的新闻列表
        public ActionResult NewsList(int page)
        {
            if (CheckSession())
            {
                int count = 5;

                List<news, int>(page, count, x => x.companyId.ToString() == Session["user_id"].ToString(), x => x.newsId);
                ViewBag.page = page + 1;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
        public ActionResult InvitationList(int page)
        {
            if (CheckSession())
            {
                int count = 5;

                List<invitation, int>(page, count, x => x.purchase.companyId.ToString() == Session["user_id"].ToString(), x => x.invitationId);
                ViewBag.page = page + 1;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
    }
}