using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

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
            return Utility.CheckSession(UserType.Company, Session);
        }

        public ActionResult Detail(int id)
        {
            var element = GetList<company>(x => x.companyId == id).SingleOrDefault();
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
                var result = GetList<company>(x => x.companyId == sessionId).SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        //获取companyId有关的列表
        private List<T> GetList<T, TKey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return Utility.GetList<T, TKey>(page, count, whereSelector, keySelector).ToList();
        }
        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> whereSelector) where T : class
        {
            return Utility.GetList<T>(whereSelector);
        }
        private int GetSumCount<T, TKey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return Utility.GetSumCount(whereSelector, keySelector);
        }

        //发布的采购信息列表
        public ActionResult PurchaseInfoList(int page)
        {
            if (CheckSession())
            {
                Assert(Session["user_id"] != null);
                Assert(Session["user_type"] != null);
                var sessionId = (Int32)Session["user_id"];
                const int count = 5;
                var list = GetList<purchase, int>(page, count,
                    x => x.companyId == sessionId,
                    x => x.purchaseId);
                ViewBag.list = list;
                ViewBag.pageSum = GetSumCount<purchase, int>(x => x.companyId == sessionId,
                    x => x.purchaseId) / count + 1;
                ViewBag.pageNum = page;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        //发布的新闻列表
        public ActionResult NewsList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;
                Assert(Session["user_id"] != null);
                Assert(Session["user_type"] != null);
                var sessionId = (Int32)Session["user_id"];
                ViewBag.list = GetList<news, int>(page, count, x => x.companyId == sessionId, x => x.newsId);
                ViewBag.pageSum = GetSumCount<news, int>(x => x.companyId == sessionId, x => x.newsId) / count + 1;
                ViewBag.pageNum = page;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        public ActionResult InvitationList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;
                Assert(Session["user_id"] != null);
                Assert(Session["user_type"] != null);
                var sessionId = (Int32)Session["user_id"];
                ViewBag.list = GetList<invitation, int>(page, count, x => x.purchase.companyId == sessionId, x => x.invitationId);
                ViewBag.pageSum = GetSumCount<invitation, int>(x => x.purchase.companyId == sessionId, x => x.invitationId) / count + 1;
                ViewBag.pageNum = page;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
    }
}