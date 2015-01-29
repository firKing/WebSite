using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class IndexStruct
    {
        public String name;
        public Pair<string, int> time;
        public String content;
        public String image;
        public int detailId;
    };
    public class TeamStruct
    {
        
    }
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
           
            ViewBag.experts =expertList.Select( record=>new IndexStruct { name = record.user.user_name, image = record.expert_image, content = record.user.user_introduction });
            ViewBag.newes = newsList.Select(record=> new IndexStruct { name = record.news_title, time = GetMonthAndDay(record.news_time) });
            ViewBag.purchases =  purchaseList.Select(record=> new IndexStruct { name = record.purchase_title, time = GetMonthAndDay( record.purchase_time)});
            ViewBag.teams = teamList.Select(record =>new IndexStruct { name = record.team_name });

            return View();
        }


        public ActionResult PurchaseList(int page)
        {
            var count = 5;
            ViewBag.list = GetList<purchase, int>(page, count, x => x.purchaseId).ToList().
                Select(x => new IndexStruct {
                    detailId = x.purchaseId,
                    name =x.purchase_title,
                    content = x.purchase_content,
                    time = new Pair<string, int>(Utility.DateTimeToString(x.purchase_time),0) } );
            ViewBag.bigtitle = "采购信息";
            ViewBag.page = page;
            ViewBag.sumPage = GetSumCount<purchase, int>(x => x.purchaseId) / count + 1;
            ViewBag.parent = "PurchaseList";

            return View("~/Views/Shared/list.cshtml");
        }
        public ActionResult NewsList(int page)
        {
            var count = 5;
            ViewBag.list = GetList<news, int>
                (page, count,x=>x.newsId).ToList().
                Select(x=>new IndexStruct {
                    detailId = x.newsId,
                    name = x.news_title,
                    content = x.news_content,
                    time = new Pair<string, int>(Utility.DateTimeToString(x.news_time),0)});
            ViewBag.sumPage = GetSumCount<news, int>(x => x.newsId)/count +1;

            ViewBag.bigtitle = "新闻列表";
            ViewBag.parent = "NewsList";
            
            ViewBag.page = page;
            
            ViewBag.pageClass = "action disabled";

            ViewBag.detail = "New";

            return View("~/Views/Shared/list.cshtml");
        }
        public ActionResult TeamList(int page)
        {
            var count = 5;
            ViewBag.list = GetList<team, int>(page, count, x => x.teamId).ToList()
                .Select(x => new IndexStruct {
                    detailId = x.teamId,
                    name = x.team_name,
                    content = x.team_introduction ,
                    time = new Pair<String, int>("",x.members.Count()) });
            ViewBag.sumPage = GetSumCount<team, int>(x => x.teamId) / count + 1;
            ViewBag.page = page;
            ViewBag.pageClass = "action disabled";

            ViewBag.bigtitle = "虚拟团队";
            ViewBag.parent = "TeamList";

            ViewBag.detail = "Team";
            return View("~/Views/Shared/list.cshtml");
        }
        public ActionResult ExpertList(int page)
        {
            var count = 8;
            ViewBag.list = GetList<expert,int>(page, count,x=>x.user_userId).ToList();
            ViewBag.sumPage = GetSumCount<team, int>(x => x.teamId) / count + 1;
            ViewBag.page = page;
            ViewBag.parent = "ExpertList";

            ViewBag.detail = "Expert";

            return View("~/Views/Expert/List.cshtml");
        }

        private IQueryable<T> GetList<T>(int countMax) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().Take(countMax);

            return container;
        }
        private IQueryable<T> GetList<T,Tkey>(int page,int count,Expression<Func<T,Tkey>> keySelector) where T : class
        {
            return Utility.GetList(page, count, keySelector);
        }
      
        public int GetSumCount<T, Tkey>(Expression<Func<T, Tkey>> keySelector) where T : class
        {
            return Utility.GetSumCount<T, Tkey>( keySelector);
        }
    }
}