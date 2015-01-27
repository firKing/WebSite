using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class IndexController : Controller
    {
        private Pair<String,int> GetMonthAndDay(System.DateTime time)
        {
            var result = new Pair<String, int>();
            result .second = time.Day); 
            switch (time.Month)
            {
                case 1:
                    result.first = "一月";
                    break;
                case 2:
                    result.first = "二月";
                    break;
                case 3:
                    result.first = "三月";
                    break;
                case 4:
                    result.first = "四月";
                    break;
                case 5:
                    result.first = "五月";
                    break;
                case 6:
                    result.first = "六月";
                    break;
                case 7:
                    result.first = "七月";
                    break;
                case 8:
                    result.first = "八月";
                    break;
                case 9:
                    result.first = "九月";
                    break;
                case 10:
                    result.first = "十月";
                    break;
                case 11:
                    result.first = "十一月";
                    break;
                case 12:
                    result.first = "十二月";
                    break;
                default:
                    break;
            }
            return result;
        }
        // GET: Index
        public ActionResult Index()
        {
            var expertList = GetList<expert>(1);
            var newsList = GetList<news>(6);
            var purchaseList = GetList<purchase>(6);
            var teamList = GetList<team>(12);
              
            ViewBag.experts = from record in expertList select new { image = record.expert_image,name = record.expert_name,introduction = record.expert_introduce };
            ViewBag.newes = from record in newsList select new {name = record.news_title, time = GetMonthAndDay(record.news_time) };
            ViewBag.purchases = from record in purchaseList select new { name = record.purchase_title, time = GetMonthAndDay(record.purchase_time) }; ;
            ViewBag.teams = from record in teamList select new { name = record.team_name };
            return View();
        }

        public ActionResult PurchaseList(int page)
        {
           var result = GetList<purchase>(page,5);
            ViewBag.list = result;
            return View();
        }
        public ActionResult NewsList(int page)
        {
            var result = GetList<news>(page, 5);
            ViewBag.list = result;
            return View();
        }
        public ActionResult ExpertList(int page)
        {
            var result = GetList<expert>(page, 5);
            ViewBag.list = result;
            return View();
        }

        private IQueryable<T> GetList<T>(int countMax) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().Take(countMax);
            return container;
        }
        private IQueryable<T> GetList<T>(int page,int count) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().Skip(page*count).Take(count);
            return container;
        }
    }
}