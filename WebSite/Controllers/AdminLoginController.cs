using System.Diagnostics.Debug;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            if (ModelState.IsValid)
            {
                var password = Utility.Md5(info.password);
                Assert(info.type != UserType.Team);
                var element = Utility.GetSingleTableRecord<admin>(x => x.admin_name == info.name &&
                                x.admin_pwd == password);
                if (element != null)
                {
                    Session["user_id"] = element.adminId;
                    Session["user_type"] = UserType.Admin;
                    return RedirectToAction("Index", "User");
                }
            }
            Assert(Request.UrlReferrer != null);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}