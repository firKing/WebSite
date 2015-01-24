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
        // GET: Admin


        public ActionResult VendorDetail(int id)
        {
            SingleTableModule<vendor> db = new SingleTableModule<vendor>();

            var element = Info<vendor>(db, x => x.vendorId == id).SingleOrDefault();
            if (element != null)
            {
                return View(element);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }
        private IQueryable<T> Info<T>(SingleTableModule<T> db,Expression<Func<T,bool>> expression)where T :class
        {
            return db.FindInfo(expression);
        }

        public ActionResult List(int id)
        {
            SingleTableModule<vendor> db = new SingleTableModule<vendor>();

            var element = Info<vendor>(db, x => x.vendorId == id);
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
        public ActionResult VendorCreate(vendor info)
        {
            SingleTableModule<vendor> db = new SingleTableModule<vendor>();
            var result = db.Create(info);
            if (result.first == false)
            {
                return RedirectToAction("Company", "VendorList");
            }
            else
            {
                return View("Detail");
            }
        }

        public ActionResult VendorDelete(int id)
        {
            SingleTableModule<vendor> db = new SingleTableModule<vendor>();
            var element = Info(db, x => x.vendorId == id).SingleOrDefault();
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
        public ActionResult VendorEdit(int id)
        {
            SingleTableModule<vendor> db = new SingleTableModule<vendor>();
            var query = db.FindInfo(x => x.vendorId == id).SingleOrDefault();
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