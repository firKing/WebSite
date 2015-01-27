using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            var expertList = GetList<expert>(1);
            var newsList = GetList<news>(6);
            var purchaseList = GetList<purchase>(6);
            var teamList = GetList<team>(12);
                
            ViewBag.experts = expertList;
            ViewBag.newses = newsList;
            ViewBag.purchases = purchaseList;
            ViewBag.teams = teamList;
            return View();
        }

        private IQueryable<T> GetList<T>(int countMax) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().Take(countMax);
            return container;
        }
    }
}