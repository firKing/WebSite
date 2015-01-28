using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class IndexController : Controller
    {
        public Pair<String,int> GetMonthAndDay(System.DateTime time)
        {
            var result = new Pair<String, int>(new String('\0',0),0);
            result .second = time.Day; 
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
            var expertList = GetList<expert>(1).ToList();
            var newsList = GetList<news>(6).ToList();
            var purchaseList = GetList<purchase>(6).ToList();
            var teamList = GetList<team>(12).ToList();

            ViewBag.experts =expertList.Select( record=>new { name = record.user.user_name, image = record.expert_image, introduction = record.user.user_introduction });
            ViewBag.newes = newsList.Select(record=> new {name = record.news_title, time = GetMonthAndDay(record.news_time) });
            ViewBag.purchases =  purchaseList.Select(record=> new { name = record.purchase_title, time = GetMonthAndDay(record.purchase_time) });
            ViewBag.teams = teamList.Select(record =>new { name = record.team_name });
            return View();
        }

        public ActionResult PurchaseList(int page)
        {
            var result = GetList<purchase,int>(page,5,x=>x.purchaseId);
            ViewBag.bigtitle = "采购信息";
            ViewBag.list = result;
            return View("~/Views/Shared/list.cshtml");
        }
        public ActionResult NewsList(int page)
        {
            var result = GetList<news, int>(page, 5,x=>x.newsId);
            ViewBag.bigtitle = "新闻列表";
            ViewBag.list = result;
            return View("~/Views/Shared/list.cshtml");
        }
        public ActionResult ExpertList(int page)
        {
            var result = GetList<expert,int>(page, 8,x=>x.user_userId);
            ViewBag.list = result;
            return View("~/Views/Expert/List.cshtml");
        }

        private IQueryable<T> GetList<T>(int countMax) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().Take(countMax);
            return container;
        }
        private IQueryable<T> GetList<T,Tkey>(int page,int count,Expression<Func<T,Tkey>> keySelector) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().OrderByDescending(keySelector).Skip(page*count).Take(count);
            return container;
        }
    }
}