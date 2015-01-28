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
    public class PurchaseController : Controller
    {
        // GET: Purchase
        private SingleTableModule<purchase> db = new SingleTableModule<purchase>();

        // GET: 
        public ActionResult Detail(int id)
        {
            var element = Info(id).SingleOrDefault();
            if (element != null)
            {
                return View(element);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        //获取采购信息详情页
        private IQueryable<purchase> Info(int purchaseId)
        {
            return db.FindInfo(x => x.purchaseId == purchaseId);
        }



        [HttpPost]
        public ActionResult Create(purchase info)
        {

            var result = db.Create(info);
            if (result.first == false)
            {
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                return View("Detail");
            }
        }
        [HttpPost]
        public ActionResult CreateInvitation(invitation info)
        {
            var dbInvitation = new SingleTableModule<invitation>();
            var result = dbInvitation.Create(info);
            return View("Detail");
        }

        public ActionResult BidList(int purachseId)
        {
            var dbBid = new SingleTableModule<bid>();
            return View(dbBid.FindInfo(x => x.purchaseId == purachseId));
        }
    }
}