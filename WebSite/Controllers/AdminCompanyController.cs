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
        public ActionResult CompanyDetail(int id)
        {
            SingleTableModule<company> db = new SingleTableModule<company>();

            var element = Info<company>(db, x => x.companyId == id).SingleOrDefault();
            if (element != null)
            {
                return View(element);
            }
            else
            {
                throw new HttpException(404, "Product not found.");
            }
        }


        public ActionResult CompanyList(int id)
        {
            SingleTableModule<company> db = new SingleTableModule<company>();

            var element = Info<company>(db, x => x.companyId == id);
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
        public ActionResult CompanyCreate(company info)
        {
            SingleTableModule<company> db = new SingleTableModule<company>();
            var result = db.Create(info);
            if (result.first == false)
            {
                return RedirectToAction("Company", "CompanyList");
            }
            else
            {
                return View("Detail");
            }
        }

        public ActionResult CompanyDelete(int id)
        {
            SingleTableModule<company> db = new SingleTableModule<company>();
            var element = Info(db, x => x.companyId == id).SingleOrDefault();
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
        public ActionResult CompanyEdit(int id)
        {
            SingleTableModule<company> db = new SingleTableModule<company>();
            var query = db.FindInfo(x => x.companyId == id).SingleOrDefault();
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