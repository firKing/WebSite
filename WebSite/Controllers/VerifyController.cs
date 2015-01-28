﻿using System;
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
        public ActionResult CheckNameExist(string name, string type)
        {
            bool result = false;
            result = (new SingleTableModule<user>())
                        .FindInfo(x => x.user_name == name &&
                                x.user_type == type)
                        .Count() == 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private UserType GetUsetTypeByString(String type)
        {
            return new Utility().GetUsetTypeByString(type);
        }
        private void SesSession(int id,String type)
        {
            new Utility().SetSession(Session, id, GetUsetTypeByString(type));

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
            new Utility().SetLoginSession(Session, userId,type);
        }
    }
}