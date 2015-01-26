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
        public ActionResult ExpertDetail(int id)
        {
            SingleTableModule<expert> db = new SingleTableModule<expert>();

            var element = Info<expert>(db, x => x.expertId == id).SingleOrDefault();
            if (element != null)
            {
                return View(element);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }


        public ActionResult ExpertList(int id)
        {
            SingleTableModule<expert> db = new SingleTableModule<expert>();

            var element = Info<expert>(db, x => x.expertId == id);
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
        public ActionResult ExpertCreate(expert info)
        {
            SingleTableModule<expert> db = new SingleTableModule<expert>();
            var result = db.Create(info);
            if (result.first == false)
            {
                return RedirectToAction("Company", "ExpertList");
            }
            else
            {
                return View("Detail");
            }
        }

        public ActionResult ExpertDelete(int id)
        {
            SingleTableModule<expert> db = new SingleTableModule<expert>();
            var element = Info(db, x => x.expertId == id).SingleOrDefault();
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
        public ActionResult ExpertEdit(int id)
        {
            SingleTableModule<expert> db = new SingleTableModule<expert>();
            var query = db.FindInfo(x => x.expertId == id).SingleOrDefault();
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