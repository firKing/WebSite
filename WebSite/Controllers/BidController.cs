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
    public class BidController : Controller
    {
        private SingleTableModule<bid> db = new SingleTableModule<bid>();
         

        // GET: 
        public ActionResult Detail(int id)
        {
            SingleTableModule<audit> dbAudit = new SingleTableModule<audit>();
            var element = Info(id).SingleOrDefault();
            if (element != null)
            {
                return View(new Pair<bid,IQueryable<audit>>
                    (element, 
                    dbAudit.FindInfo(x => x.bidId == id)));
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        //获取标书详情
        private IQueryable<bid> Info(int id)
        {
            return db.FindInfo(x => x.bidderId == id);
        }



        [HttpPost]
        public ActionResult Create(bid info)
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
        public ActionResult CreateAudit(audit info)
        {
            SingleTableModule<audit> dbAudit = new SingleTableModule<audit>();
            var result = dbAudit.Create(info);
            if (result.first == false)
            {
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                return View("Detail");
            }
        }
    }
}