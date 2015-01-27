using System;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers
{
    //验证逻辑的ajax服务器端.
    public class VerifyController : Controller
    {
        private bool CheckNameExist<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return (new SingleTableModule<T>())
                        .FindInfo(expression)
                        .Count() == 1;
        }
        
        private bool CheckLogin<T>(Expression<Func<T, bool>> checkName, Expression<Func<T, bool>> checkPassword,UserType type) where T : class
        {
            var tableModule = new SingleTableModule<T>();
            var query = tableModule.FindInfo().
                Where(checkName).
                Where(checkPassword);
            var result = false;
            var element = query.SingleOrDefault();
            if (element != null)
            {
                result = true;
                Session["user_id"] = tableModule.GetRecordId(element);
                Session["user_type"] = type.ToString();
            }
            return result;
        }
        //romote vailation
        public ActionResult CheckNameExist(string name, UserType type)
        {
            bool result = false;
            switch (type)
            {
                case UserType.Expert:
                    result = CheckNameExist<expert>(x => x.user.user_name == name);
                    break;

                case UserType.Company:
                    result = CheckNameExist<company>(x => x.user.user_name == name);
                    break;

                case UserType.Vendor:
                    result = CheckNameExist<vendor>(x => x.user.user_name == name);
                    break;

                default:
                    break;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //ajax
        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                Assert(info.type != UserType.Team);
                switch (info.type)
                {
                    case UserType.Expert:
                        result = CheckLogin<expert>(
                            x => x.user.user_name == info.name,
                            x => x.user.user_password == info.password,
                            info.type);
                        break;

                    case UserType.Company:
                        result = CheckLogin<company>(
                            x => x.user.user_name == info.name,
                            x => x.user.user_password == info.password,
                             info.type);
                        break;

                    case UserType.Vendor:
                        result = CheckLogin<vendor>(
                            x => x.user.user_name == info.name,
                            x => x.user.user_password == info.password,
                             info.type);
                        break;

                    default:
                        break;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}