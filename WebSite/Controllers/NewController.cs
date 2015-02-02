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
using System.EnterpriseServices.Internal;

namespace WebSite.Controllers
{
    public class NewController : Controller
    {
        private bool CheckSession()
        {
            return Utility.CheckSession(UserType.Company, Session);
        }

        public ActionResult Publish()
        {
            if (CheckSession())
            {
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> whereSelector) where T : class
        {
            return Utility.GetList<T>(whereSelector);
        }
        private Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return Utility.CreateRecord(record);
        }
        // GET: NewList
        public ActionResult Detail(int id)
        {
            var element = GetList<news>(x => x.newsId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.name = element.news_title;
                ViewBag.content = element.news_content;
                ViewBag.time = element.news_time;
                ViewBag.creator = element.company.user.user_name;
                return View("~/Views/Shared/detail.cshtml");
            }
            else
            {
                return HttpNotFound();
            }
        }

        //获取新闻详情页
     

        [HttpGet]
        public ActionResult Create()
        {
            var record = new news();
            return View(record);
        }

        [HttpPost]
        public ActionResult Create(news info)
        {
            if (ModelState.IsValid&&Utility.CheckSession(UserType.Company,Session))
            {
                info.company = Utility.GetForiegnKeyTableRecord<company>(x => x.companyId == (Int32)Session["user_id"]);
               CreateRecord<news>(info);
                return View("Detail");
            }
            return RedirectToAction("Home", "Company");
        }

        [HttpPost]
        //ajax删除新闻
        public ActionResult Delete(int id)
        {
            var result = false;
            var element = GetList<news>(x => x.newsId == id).SingleOrDefault();
            if (element != null)
            {
                result = new SingleTableModule<news>().Delete(element);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var query = GetList<news>(x => x.newsId == id).SingleOrDefault();
            if (query == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("Create", query);
            }
        }
    }
}