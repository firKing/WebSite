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

        private Dictionary<String, FindTableRecordHandler> GetFindTableRecordMap()
        {
            var findTableRecordMap = new Dictionary<String, FindTableRecordHandler>();
            findTableRecordMap.Add(UserType.Expert.ToString(), (int id) =>
            {
                var result = new SingleTableModule<expert>().FindInfo(x => x.expertId == id).SingleOrDefault();
                return result;
            });
            findTableRecordMap.Add(UserType.Company.ToString(), (int id) =>
            {
                var result = new SingleTableModule<company>().FindInfo(x => x.companyId == id).SingleOrDefault();
                return result;

            });
            findTableRecordMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                var result = new SingleTableModule<vendor>().FindInfo(x => x.vendorId == id).SingleOrDefault();
                return result;

            });
            return findTableRecordMap;
        }
        delegate object FindTableRecordHandler(int id);
        public ActionResult Edit(int user_id,String user_type)
        {
            var findResult = GetFindTableRecordMap()[user_type](user_id);
            if (findResult != null)
            {
                ViewBag.element = findResult;
                return View("~/Views/Register/Index.cshtml");
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