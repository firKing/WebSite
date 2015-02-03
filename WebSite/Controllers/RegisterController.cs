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

        private bool IsLoginUser()
        {
            return Utility.CheckSession(UserType.Company, Session) ||
                Utility.CheckSession(UserType.Expert, Session)||
                Utility.CheckSession(UserType.Vendor, Session);
        }
        public ActionResult Edit(int user_id, String user_type)
        {
            if (IsLoginUser())
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

        [HttpPost]
        public ActionResult Register(user info, String authCode)
        {
            var validateCode = Convert.ToString(Session["ValidateCode"]);
            Assert(validateCode != null);
            if (validateCode == authCode)
            {
                bool isEdit = (info.userId == 0) ? false : true;
                var password = Utility.Md5(info.user_password);
                if (!isEdit)
                {
                    info.user_password = password;
                }
                var createResult =
                    (!isEdit) ?
                    CreateRecord<user>(info) :
                  Utility.EditRecord<user>(x=>x.userId==info.userId, x =>
                    {
                        x.user_address = info.user_address;
                        x.user_introduction = info.user_introduction;
                        x.user_mail = info.user_mail;
                        x.user_telephone = x.user_telephone;
                        return x;
                    });
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