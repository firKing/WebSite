using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        public ActionResult Index()
        {
            return View(new user { });
        }

        private delegate object FindTableRecordHandler(int id);

        private Dictionary<String, FindTableRecordHandler> GetFindTableRecordMap()
        {
            var findTableRecordMap = new Dictionary<String, FindTableRecordHandler>();
            findTableRecordMap.Add(UserType.Expert.ToString(), (int id) =>
            {
                var result = Utility.GetList<expert>(x => x.expertId == id).SingleOrDefault();
                return result;
            });
            findTableRecordMap.Add(UserType.Company.ToString(), (int id) =>
            {
                var result = Utility.GetList<company>(x => x.companyId == id).SingleOrDefault();
                return result;
            });
            findTableRecordMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                var result = Utility.GetList<vendor>(x => x.vendorId == id).SingleOrDefault();
                return result;
            });
            return findTableRecordMap;
        }

        public ActionResult Edit(int user_id, String user_type)
        {
            var findResult = GetFindTableRecordMap()[user_type](user_id);
            if (findResult != null)
            {
                switch (user_type)
                {
                    case "Company":
                        return View("Edit", ((company)findResult).user);

                    case "Expert":
                        return View("Edit", ((expert)findResult).user);

                    case "Vendor":
                        return View("Edit", ((vendor)findResult).user);
                }
            }
            return HttpNotFound();
        }

        private bool CheckUserType(String type)
        {
            return Utility.CheckUserType(type);
        }

        private Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return Utility.CreateRecord(record);
        }

        private bool EditRecord<T>(T record) where T : class
        {
            return Utility.EditRecord(record);
        }

        [HttpPost]
        public ActionResult Register(user info, String authCode)
        {
            var validateCode = Convert.ToString(Session["ValidateCode"]);
            Assert(validateCode != null);
            if (ModelState.IsValid && validateCode == authCode)
            {
                bool isEdit = false;
                isEdit = (info.userId == 0) ? false : true;

                var createResult =
                    (!isEdit) ?
                    CreateRecord<user>(info) :
                    new Pair<bool, user>(EditRecord<user>(info)
                    , info);
                
                if (createResult.first)
                {
                    var findIter = Utility.GetList<user>(x => x.userId == createResult.second.userId).SingleOrDefault();
                    Assert(findIter != null);
                    Assert(CheckUserType(findIter.user_type));
                    if (!isEdit)
                    {
                        Utility.RegisterUserTypeTable(findIter.userId, findIter.user_type);
                    }
                  
                    SetLoginSession(findIter.userId, findIter.user_type);
                }
            }

            return RedirectToAction("Index", "Index");
        }

        private void SetLoginSession(int userId, string type)
        {
            Utility.SetLoginSession(Session, userId, type);
        }
    }
}