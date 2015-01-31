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

        [HttpPost]
        //romote vailation
        public ActionResult CheckRegisterNameExist(string name, string type)
        {
            var result = CheckNameExist<user>(name, x => x.user_name == name && x.user_type == type);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private bool CheckNameExist<T>(string name,Expression<Func<T,bool>> whereSelector )where T :class
        {
            return (new SingleTableModule<T>())
                        .FindInfo(whereSelector)
                        .Count() == 1;
        }
        //EF框架太挫,不得不重复代码,艹
        private List<String> CheckListNameMemberExist(List<String> nameList)
        {
            var nonExistList = new List<String>();

            foreach (var name in nameList)
            {
                var result = CheckNameExist<member>(name, x => 
                 x.vendor.user.user_name == name);
                if (result == false)
                {
                    nonExistList.Add(name);
                }
            }
            return nonExistList;
        }
        private List<String> CheckListNameExpertExist(List<String> nameList)
        {
            var nonExistList = new List<String>();

            foreach (var name in nameList)
            {
                var result = CheckNameExist<expert>(name, x =>
                    x.user.user_name == name);
                if (result == false)
                {
                    nonExistList.Add(name);
                }
            }
            return nonExistList;
        }
        //原本优雅的写法- -...
        //private List<String> CheckListNameExist<T>(List<String> nameList,Func<T,String> keySelector)where T :class
        //{
        //    var nonExistList = new List<String>();

        //    foreach (var name in nameList)
        //    {
        //        var result = CheckNameExist<T>(name,x => keySelector.Invoke(x) == name);
        //        if (result == false)
        //        {
        //            nonExistList.Add(name);
        //        }
        //    }
        //    return nonExistList;
        //}
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
            //CheckListNameExist<expert>
            //(nameList, x =>
            //    x.user.user_name);
            return Json(nonExistList, JsonRequestBehavior.AllowGet);
        }

        private UserType GetUsetTypeByString(String type)
        {
            return Utility.GetUsetTypeByString(type);
        }
        private void SesSession(int id,String type)
        {
            Utility.SetSession(Session, id, GetUsetTypeByString(type));
        }
        //ajax
        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                Assert(info.type != UserType.Team);
                var element = (new SingleTableModule<user>())
                        .FindInfo(x => x.user_name == info.name &&
                                x.user_type== info.type.ToString() &&
                                x.user_password == info.password).SingleOrDefault();
                if (element != null)
                {
                    result = true;
                    SetLoginSession(element.userId,element.user_type);
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
        private void SetLoginSession(int userId,String type)
        {
            Utility.SetLoginSession(Session, userId,type);
        }
    }
}