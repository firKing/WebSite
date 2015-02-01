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
                ViewBag.detailActionName = "Team";
                return View("~/Views/Shared/detail.cshtml");
            }
            else
            {
                                return HttpNotFound();
            }
        }
        //参数 json teamId:teamIdValue
        //返回值true和false
        [HttpPost]
        public ActionResult AddTeam(int teamId)
        {
            var result = false;
            if (CheckSession())
            {
                var db = new SingleTableModule<member>();
                var record = new member();
                record.teamId = teamId;
                record.vendorId = (Int32)Session["user_id"];
                result =  db.Create(record).first;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private bool CheckSession()
        {
            return Utility.CheckSession(UserType.Vendor, Session);
        }
        // 提交创建虚拟团队
        // 成员姓名， 逗号分隔 group_name
        // 采购对象 purchase_object
        // 概要 summary
        private void CreateMembers(int teamId, List<String> nameList)
        {
            //Assert ExpertName Exsit

            var vendorTable = new SingleTableModule<vendor>();

            Assert(nameList
                .Select(x =>
                vendorTable.FindInfo(y =>
                    y.user.user_name == x &&
                    y.user.user_type == UserType.Expert.ToString())
                    .SingleOrDefault() != null)
                    .Aggregate((x, y) =>
                        x == true &&
                        y == true));

            var vendorIdList = nameList
                .Select(x =>
                vendorTable.FindInfo(y =>
                y.user.user_name == x &&
                y.user.user_type == UserType.Expert.ToString())
                .SingleOrDefault().vendorId).ToList();
            var memberTable = new SingleTableModule<member>();
            foreach (var vendorId in vendorIdList)
            {
                memberTable.Create(new member
                {
                    teamId = teamId,
                    vendorId = vendorId
                });
            }
        }
        [HttpPost]
        public ActionResult Create(team info,String memberNames, bid bidinfo)
        {
            if (CheckSession()&&ModelState.IsValid)
            {
                var table = new SingleTableModule<team>();
                var result = table.Create(info);
                if(result.first == true)
                {
                    var memberNmaeList = memberNames.Split(',').ToList();
                    CreateMembers(result.second.teamId,memberNmaeList);
                    var bidderResult = Utility.CreateBidder(result.second.teamId, UserType.Team);
                    if (bidderResult.first == true)
                    {
                        bidinfo.bidderId = bidderResult.second.bidderId;
                        var bidTable = new SingleTableModule<bid>();
                        var bidResult = bidTable.Create(bidinfo);
                        if (bidResult.first == true)
                        {
                            return RedirectToAction("Detail", "Bid", new { id = bidResult.second.bidId });
                        }
                    }
                }
            }
            return RedirectToAction("Detail","Purchase",new {id = info.purchaseId });
        }
        
    }
}