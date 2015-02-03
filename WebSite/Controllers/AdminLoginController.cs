using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Login(LoginModel info)
        {
            if (ModelState.IsValid)
            {
                var password = Utility.Md5(info.password);
                Assert(info.type != UserType.Team);
                var element = Utility.GetList<admin>(x => x.admin_name == info.name &&
                                x.admin_pwd ==password ).SingleOrDefault();
                if (element != null)
                {
                    return RedirectToAction("Index", "User");
                }
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}