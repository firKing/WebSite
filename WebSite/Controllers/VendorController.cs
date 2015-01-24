using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;

namespace WebSite.Controllers
{
    public class VendorController : Controller
    {
        // GET: VendorHome
        public ActionResult Index()
        {
            return Info();
        }
        public ActionResult Home()
        {
            return Index();
        }
        public bool CheckSession()
        {
            return new Utility().CheckSession(UserType.Vendor, Session);
        }

        public ActionResult Info()
        {
            if (CheckSession())
            {
                var table = new SingleTableModule<vendor>();
                var result = table.
                    FindInfo(x => x.vendorId == Convert.ToInt32(Session["user_id"]))
                    .SingleOrDefault();
                Assert(result != null);
                return View(result);
            }
            return RedirectToAction("Index", "Index");
        }
    }
}