using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class TeamController : Controller
    {
        //创建虚拟团队

        // GET: Team
        public ActionResult Index(int purchaseId)
        {
            if (CheckSession())
            {
                ViewBag.purchaseTitle = Utility.GetSingleTableRecord<purchase>(x => x.purchaseId == purchaseId).purchase_title;
                return View();
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }

        private Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return Utility.CreateRecord(record);
        }

        //团队详情
        public ActionResult Detail(int id)
        {
            //名字,内容,创建公司.时间
            var element = Utility.GetList<team>(x => x.teamId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.name = element.team_name;
                ViewBag.content = element.team_introduction;
                ViewBag.time = "";
                ViewBag.teamId = element.teamId;
                ViewBag.creator = Utility.GetSingleTableRecord<vendor>(x => x.vendorId == element.createId).user.user_name;
                ViewBag.detailActionName = "Team";
                return View("~/Views/Shared/detail.cshtml");
            }
            else
            {
                return HttpNotFound();
            }
        }

        //参数 json teamId:teamIdValue
        //返回值"true"和"false"
        [HttpPost]
        public ActionResult AddTeam(int teamId)
        {
            var result = false;
            if (CheckSession())
            {
                var record = new member();
                record.teamId = teamId;
                record.vendorId = (Int32)Session["user_id"];
                result = CreateRecord<member>(record).first;
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
            Assert(nameList
                .Select(x =>
                Utility.GetList<vendor>(y =>
                    y.user.user_name == x &&
                    y.user.user_type == UserType.Expert.ToString())
                    .SingleOrDefault() != null)
                    .Aggregate((x, y) =>
                        x &&
                        y));
            var vendorIdList = nameList
                .Select(x =>
                Utility.GetList<vendor>(y =>
                y.user.user_name == x &&
                y.user.user_type == UserType.Expert.ToString())
                .SingleOrDefault().vendorId).ToList();
            foreach (var iter in vendorIdList)
            {
                var vendorId = iter;
                CreateRecord<member>(new member
                {
                    teamId = teamId,
                    vendorId = vendorId,
                    //  team = Utility.GetSingleTableRecord<team>(x=>x.teamId == teamId),
                    //  vendor = Utility.GetSingleTableRecord<vendor>(x=>x.vendorId == vendorId),
                });
            }
        }

        private String UploadFileGetUrl(bid info)
        {
            return Utility.UploadFileGetUrl(info, Request);
        }

        public ActionResult Create(TeamModel model)
        {
            team info = model.info;
            String memberNames = model.memberNames;
            bid bidInfo = model.bidInfo;
            if (CheckSession() /*&& ModelState.IsValid*/)
            {
                var result = CreateRecord<team>(info);
                if (result.first)
                {
                    var memberNmaeList = memberNames.Split(',').ToList();
                    CreateMembers(result.second.teamId, memberNmaeList);
                    var bidderResult = Utility.CreateBidder(result.second.teamId, UserType.Team);
                    if (bidderResult.first)
                    {
                        Utility.FillBidRecord(bidInfo, bidderResult.second, Request);
                        var bidResult = CreateRecord<bid>(bidInfo);
                        if (bidResult.first)
                        {
                            return RedirectToAction("Detail", "Bid", new { id = bidResult.second.bidId });
                        }
                    }
                }
            }
            return RedirectToAction("Detail", "Purchase", new { id = info.purchaseId });
        }
    }
}