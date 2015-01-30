using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class TeamController : Controller
    {
        //创建虚拟团队
        
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }
        //团队详情
        public ActionResult Detail(int id)
        {
            //名字,内容,创建公司.时间
            SingleTableModule<team> db = new SingleTableModule<team>();
            var element = db.FindInfo(x => x.teamId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.name = element.team_name;
                ViewBag.content = element.team_introduction;
                ViewBag.time = "";
                ViewBag.creator = element.purchase.company.user.user_name;
                return View("~/Views/Shared/detail.cshtml");
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
    }
}