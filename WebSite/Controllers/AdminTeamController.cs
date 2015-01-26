using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Web;
using System.Linq.Expressions;

namespace WebSite.Controllers
{
    public partial class AdminVendorController : Controller
    {
        public ActionResult TeamDetail(int id)
        {
            SingleTableModule<team> db = new SingleTableModule<team>();

            var element = Info<team>(db, x => x.teamId == id).SingleOrDefault();
            if (element != null)
            {
                return View(element);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
       

        public ActionResult TeamList(int id)
        {
            SingleTableModule<team> db = new SingleTableModule<team>();

            var element = Info<team>(db, x => x.teamId == id);
            if (element != null)
            {
                return View(element);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }

        [HttpPost]
        public ActionResult TeamCreate(team info)
        {
            SingleTableModule<team> db = new SingleTableModule<team>();
            var result = db.Create(info);
            if (result.first == false)
            {
                return RedirectToAction("Company", "TeamList");
            }
            else
            {
                return View("Detail");
            }
        }

        public ActionResult TeamDelete(int id)
        {
            SingleTableModule<team> db = new SingleTableModule<team>();
            var element = Info(db, x => x.teamId == id).SingleOrDefault();
            if (element != null)
            {
                db.Delete(element);
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        public ActionResult TeamEdit(int id)
        {
            SingleTableModule<team> db = new SingleTableModule<team>();
            var query = db.FindInfo(x => x.teamId == id).SingleOrDefault();
            if (query == null)
            {
                throw new HttpException(404, "Product not found.");
            }
            else
            {
                return View("Create", query);
            }

        }
    }
}