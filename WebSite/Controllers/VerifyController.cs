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
    //验证逻辑的ajax服务器端.
    public class VerifyController : Controller
    {
        //romote vailation
        //检测用户名是否存在,ajax 第一个参数是用户名,第二个是用户类型,
        //json {name:nameValue,type:typeValue};
        //返回值,"true" 用户已存在  "false" 用户不存在
        [HttpPost]
        public JsonResult CheckRegisterNameExist(string user_name, string user_type)
        {
            var result = CheckNameExist<user>(x => x.user_name == user_name && x.user_type == user_type);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        private bool CheckNameExist<T>(Expression<Func<T, bool>> whereSelector) where T : class
        {
            return Utility.GetList<T>(whereSelector)
                        .Count() == 1;
        }

        //EF框架太挫,不得不重复代码,艹
        private List<String> CheckListNameMemberExist(List<String> nameList)
        {
            return nameList.Where(
                      x =>
                          !CheckNameExist<vendor>(
                              y =>
                                  y.user.user_name == x)).ToList();
        }

        private List<String> CheckListNameExpertExist(List<String> nameList)
        {
            return nameList.Where(
                x =>
                    !CheckNameExist<expert>(
                        y =>
                            y.user.user_name == x)).ToList();
        }
        [HttpPost]
        public ActionResult CheckMemberListNameExist(string names)
        {
            var nameList = names.Split(',').ToList();
            var nonExistList =
                CheckListNameMemberExist
                (nameList);
            return Json(nonExistList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckExpertListNameExist(string names)
        {
            var nameList = names.Split(',').ToList();
            var nonExistList =
                CheckListNameExpertExist
                (nameList);
            return Json(nonExistList, JsonRequestBehavior.AllowGet);
        }

        //ajax
        //参数是LoginModel类型的各个字段是json对象的key
        //返回的是json 字符串
        //"true""false"
        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                Assert(info.type != UserType.Team);
                var password = Utility.Md5(info.password);
                var element = Utility.GetList<user>(x => x.user_name == info.name &&
                                x.user_type == info.type.ToString() &&
                                x.user_password == password).SingleOrDefault();
                if (element != null)
                {
                    result = true;
                    SetLoginSession(element.userId, element.user_type);
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

        private void SetLoginSession(int userId, String type)
        {
            Utility.SetLoginSession(Session, userId, type);
        }

        public ActionResult LoginOut()
        {
            Utility.ClearSession(Session);
            return RedirectToAction("Index", "Index");
        }
    }
}