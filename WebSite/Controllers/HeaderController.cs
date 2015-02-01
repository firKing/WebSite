using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HeaderController : Controller
    {
        private delegate String HeaderEventHandler(int user_id);

        private readonly Dictionary<UserType, HeaderEventHandler> handerEventMap = new Dictionary<UserType, HeaderEventHandler>();

        // GET: Header
        private void InithanderEventMap()
        {
            handerEventMap.Add(UserType.Expert, (int id) =>
            {
                var result = new SingleTableModule<expert>().FindInfo(x => x.expertId == id).SingleOrDefault();
                Assert(result != null);
                return result.user.user_name;
            });
            handerEventMap.Add(UserType.Vendor, (int id) =>
            {
                var result = new SingleTableModule<vendor>().FindInfo(x => x.vendorId == id).SingleOrDefault();
                Assert(result != null);
                return result.user.user_name;
            });
            handerEventMap.Add(UserType.Company, (int id) =>
            {
                var result = new SingleTableModule<company>().FindInfo(x => x.companyId == id).SingleOrDefault();
                Assert(result != null);
                return result.user.user_name;
            });
        }

        public ActionResult Index()
        {
            if (Session["user_id"] == null || Session["user_type"] == null)
            {
                ViewBag.userName = "注册";
                ViewBag.login = false;
            }
            else
            {
                InithanderEventMap();
                var id = (Int32)Session["user_id"];
                var type = (UserType)Session["user_type"];
                ViewBag.userName = handerEventMap[type](id);
                ViewBag.login = true;
                ViewBag.userType = type;
                ViewBag.id = id;
            }
            return PartialView("~/Views/Shared/header.cshtml");
        }
    }
}