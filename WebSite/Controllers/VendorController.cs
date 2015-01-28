using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Collections.Generic;

namespace WebSite.Controllers
{
    public class VendorController : Controller
    {
        // GET: VendorHome
        public ActionResult Home()
        {
            return Info();
        }
       


        public ActionResult Info()
        {
            if (CheckSession())
            {
                var query = (IQueryable<user>)GetList<user>(x => x.userId);
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.vendor = result;
                return View(result);
            }
            return RedirectToAction("Index", "Index");
        }

        // 提交创建虚拟团队
        // 成员姓名， 逗号分隔 group_name
        // 采购对象 purchase_object
        // 概要 summary
        [HttpPost]
        public ActionResult CreateTeam(team info)
        {
            if (CheckSession())
            {
                var table = new SingleTableModule<team>();
                table.Create(info);
            }            
            return RedirectToAction("Home", "Company");
        }
                   
        //加入的团队列表
        public ActionResult AddTeamList()
        {
            if (CheckSession())
            {
                var table = new SingleTableModule<member>();
                var teamTable = new SingleTableModule<team>();
                var result = new List<Pair<team, IQueryable<member>>>();
                var query = (IQueryable<member>)GetList<member>(x => x.vendorId);
                foreach (var iter in query)
                {
                   var first = teamTable.FindInfo(x => x.teamId == iter.teamId).SingleOrDefault();
                   var second= table.FindInfo(x => x.teamId == iter.teamId);
                   result.Add(new Pair<team, IQueryable<member>>(first,second));
                }
                ViewBag.troop = result;
                return View(result);
            }
            return RedirectToAction("Index", "Index");

        }
        public ActionResult CreateTeamList()
        {
            if (CheckSession())
            {
               
                var result = new List<Pair<team, IQueryable<member>>>();
                var query = (IQueryable<team>)GetList<team>(x => x.createId);
                var table = new SingleTableModule<member>();
                foreach (var iter in query)
                {
                    result.Add(new Pair<team, IQueryable<member>>(iter,table.FindInfo(x => x.teamId == iter.teamId)));
                }
                return View(result);
            }
            return RedirectToAction("Index", "Index");

        }
        public ActionResult PublishBidList()
        {

            if (CheckSession())
            {

                var table = new SingleTableModule<bid>();
                var id = GetBidderId(Convert.ToInt32(Session["user_id"]));

                ViewBag.personal = table.FindInfo(x => x.bidderId == id).Select(x=> new {title = x.purchase.purchase_title,name = x.bid_title ,hit = x.purchase.hitId == x.bidId?true : false});

                return View();
            }
            return RedirectToAction("Index", "Index");

        }

        private int GetBidderId(int venderId)
        {

            var table = new SingleTableModule<bidder>();
            var elelemt = table.FindInfo(x => x.tendererId == venderId).SingleOrDefault();
            Assert(elelemt == null);
            return elelemt.bidderId;
        }
        private object GetList<T>(Func<T, int> expression) where T : class
        {
            return new Utility().GetList<T>(expression, Session);
        }
        private bool CheckSession()
        {
            return new Utility().CheckSession(UserType.Vendor, Session);
        }
        /*
        M我加入的虚拟团队列表 			pair<model<team>,List<model<member>>> GetAddVirtualTeamList(int vendorId);查memeber表,查team表
	M我创建的虚拟团队列表 			pair<model<team>,List<model<member>>> GetCreatedVirtualTeamList(int vendorId);
memeber team
	M查看发布的投标列表 			List<model<bid>>GetPublishBidList(int vendorId);

        */

    }
}