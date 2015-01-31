using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Linq.Expressions;
namespace WebSite.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
      
        public ActionResult Index()
        {
            return View();
        }
        delegate object FindTableRecordHandler(int id);
        public ActionResult Index(int user_id,String user_type)
        {
            var findTableRecordMap = new Dictionary<String, FindTableRecordHandler>();
            findTableRecordMap.Add(UserType.Expert.ToString(), (int id) =>
            {
                var result = new SingleTableModule<expert>().FindInfo(x => x.user_userId == id).SingleOrDefault();
                return result;
            });
            findTableRecordMap.Add(UserType.Company.ToString(), (int id) =>
            {
                var result = new SingleTableModule<company>().FindInfo(x => x.user_userId == id).SingleOrDefault();
                return result;

            });
            findTableRecordMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                var result = new SingleTableModule<vendor>().FindInfo(x => x.user_userId == id).SingleOrDefault();
                return result;

            });
            var findResult = findTableRecordMap[user_type](user_id);
            if (findResult != null)
            {
                ViewBag.element = findResult;
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }


        private bool CheckUserType(String type)
        {
            return Utility.CheckUserType(type);
        }
   
        [HttpPost]
        public ActionResult Register(user info,String authCode)
        {
            var validateCode = Convert.ToString(Session["ValidateCode"]);
            Assert(validateCode!=null);
            if (ModelState.IsValid && validateCode == authCode)
            {
                var table = new SingleTableModule<user>();
                var createResult = table.Create(info);
                if (createResult.first == true)
                {
                    var findIter = table.FindInfo(x => x.userId == createResult.second.userId).SingleOrDefault();
                    Assert(findIter != null);
                    Assert(CheckUserType(findIter.user_type));
                    Utility.RegisterUserTypeTable(findIter.userId, findIter.user_type);
                    SetLoginSession(findIter.userId, findIter.user_type);
                }
            }

            return RedirectToAction("Index","Index");
        }
        private void SetLoginSession(int userId,string type)
        {
            Utility.SetLoginSession(Session,userId,type);
        }

    }
}