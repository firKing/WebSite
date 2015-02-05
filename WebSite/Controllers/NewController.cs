using System;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

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

        private Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return Utility.CreateRecord(record);
        }

        // GET: NewList
        public ActionResult Detail(int id)
        {
            var element = Utility.GetSingleTableRecord<news>(x => x.newsId == id);
            if (element != null)
            {
                ViewBag.name = element.news_title;
                ViewBag.content = Utility.HtmlDecode(element.news_content);
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
            if (ModelState.IsValid && Utility.CheckSession(UserType.Company, Session))
            {
                info.news_time = DateTime.Now;
                info.companyId = (Int32)Session["user_id"];
                info.news_content = HttpUtility.HtmlEncode(info.news_content);
                if (CreateRecord<news>(info).first)
                {
                    return RedirectToAction("Detail", "New", new { id = info.newsId });
                }
            }
            return RedirectToAction("Home", "Company");
        }

        [HttpPost]
        //ajax删除新闻
        public ActionResult Delete(int id)
        {
            var result = false;
            var element = Utility.GetSingleTableRecord<news>(x => x.newsId == id);
            if (element != null)
            {
                result = new SingleTableModule<news>().Delete(element);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var query = Utility.GetSingleTableRecord<news>(x => x.newsId == id);
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