using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class NewController : Controller
    {
        private SingleTableModule<news> db = new SingleTableModule<news>();

        // GET: NewList
        public ActionResult Detail(int id)
        {
            var element = Info(id).SingleOrDefault();
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
        private IQueryable<news> Info(int newsId)
        {
            return db.FindInfo(x => x.newsId == newsId);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var record = new news();
            return View(record);
        }

        [HttpPost]
        public ActionResult Create(news info)
        {
            if (ModelState.IsValid)
            {
                db.Create(info);
                return View("Detail");
            }
            return RedirectToAction("Home", "Company");
        }

        [HttpPost]
        //ajax删除新闻
        public ActionResult Delete(int id)
        {
            var result = false;
            var element = Info(id).SingleOrDefault();
            if (element != null)
            {
                result = db.Delete(element);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var query = db.FindInfo(x => x.newsId == id).SingleOrDefault();
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