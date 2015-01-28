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
        public ActionResult CheckNameExist(string name, string type)
        {
            bool result = false;
            result = (new SingleTableModule<user>())
                        .FindInfo(x => x.user_name == name &&
                                x.user_type == type)
                        .Count() == 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //ajax
        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            var result = false;
            if (ModelState.IsValid)
            {
               // Assert(info.type != UserType.Team.ToString());
                var element = (new SingleTableModule<user>())
                        .FindInfo(x => x.user_name == info.name &&
                                x.user_type.ToString() == info.type.ToString());
                if (element != null && element.Count() == 1)
                {
                    var target = element.SingleOrDefault();
                    Session["user_type"] = target.user_type;
                    Session["user_id"] = target.userId;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CheckValidCode(String validCode)
        {
            var result = false;
            String session = Convert.ToString(Session["ValidateCode"]);
            if (!String.IsNullOrEmpty(session) && validCode == session)
            {
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);

            return File(bytes, @"image/jpeg");
        }

      
    }
}