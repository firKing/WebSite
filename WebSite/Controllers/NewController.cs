using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Web;

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
                ViewBag.news = element;
                return View();
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        //获取新闻详情页
        private IQueryable<news> Info(int newsId)
        {
            return db.FindInfo(x => x.newsId == newsId);
        }

       

        [HttpPost]
        public ActionResult Create(news info)
        {

            var result = db.Create(info);
            if (result.first == false)
            {
                return RedirectToAction("Company", "NewsList");
            }
            else
            {
                return View("Detail");
            }
        }

        public ActionResult Delete(int id)
        {

            var element = Info(id).SingleOrDefault();
            if (element != null)
            {
                db.Delete(element);
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var query = db.FindInfo(x => x.newsId == id).SingleOrDefault();
            if (query == null)
            {
                throw new HttpException(404, "Product not found.");
            }
            else
            {
                return View("Create", query);
            }

        }
    }
}