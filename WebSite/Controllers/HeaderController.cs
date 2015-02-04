using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Common;
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
                var result = Utility.GetSingleTableRecord<expert>(x => x.expertId == id);
                Assert(result != null);
                return result.user.user_name;
            });
            handerEventMap.Add(UserType.Vendor, (int id) =>
            {
                var result = Utility.GetSingleTableRecord<vendor>(x => x.vendorId == id);
                Assert(result != null);
                return result.user.user_name;
            });
            handerEventMap.Add(UserType.Company, (int id) =>
            {
                var result = Utility.GetSingleTableRecord<company>(x => x.companyId == id);
                Assert(result != null);
                return result.user.user_name;
            });
            handerEventMap.Add(UserType.Admin, (int id) =>
            {
                var result = Utility.GetSingleTableRecord<admin>(x => x.adminId == id);
                Assert(result != null);
                return result.admin_name;
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

                var userName = handerEventMap[type](id);
                if (userName.Count() > 6)
                {
                    userName = userName.Substring(0, 6) + "...";
                }
                ViewBag.userName = userName;
                ViewBag.login = true;
                ViewBag.userType = type;
                ViewBag.id = id;
            }
            return PartialView("~/Views/Shared/header.cshtml");
        }
    }
}