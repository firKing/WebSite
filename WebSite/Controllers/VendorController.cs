using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;

namespace WebSite.Controllers
{
    public class VendorController : Controller
    {
        // GET: VendorHome
        public ActionResult Home()
        {
            return Info();
        }

        public ActionResult Detail(int id)
        {
            var db = new SingleTableModule<vendor>();

            var element = db.FindInfo(x => x.vendorId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.name = element.user.user_name;
                ViewBag.content = element.user.user_introduction;
                return View("~/Views/Shared/detail.cshtml");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Info()
        {
            if (CheckSession())
            {
                var sessionId = Convert.ToInt32(Session["user_id"]);

                var query = GetList<vendor>(x => x.vendorId == sessionId);
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.vendor = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }

       
                   
        //加入的团队列表
        public ActionResult AddTeamList()
        {
            if (CheckSession())
            {
                var table = new SingleTableModule<member>();
                var teamTable = new SingleTableModule<team>();
                var result = new List<Pair<team, IQueryable<member>>>();
                var sessionId = Convert.ToInt32(Session["user_id"]);

                var query = (IQueryable<member>)GetList<member>(x => x.vendorId== sessionId);
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
                var sessionId = Convert.ToInt32(Session["user_id"]);

                var query = (IQueryable<team>)GetList<team>(x => x.createId == sessionId);
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
        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return Utility.GetList<T>(expression);
        }
        private bool CheckSession()
        {
            return Utility.CheckSession(UserType.Vendor, Session);
        }
        /*
        M我加入的虚拟团队列表 			pair<model<team>,List<model<member>>> GetAddVirtualTeamList(int vendorId);查memeber表,查team表
	M我创建的虚拟团队列表 			pair<model<team>,List<model<member>>> GetCreatedVirtualTeamList(int vendorId);
memeber team
	M查看发布的投标列表 			List<model<bid>>GetPublishBidList(int vendorId);

        */

    }
}