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
        private delegate void RegisterEventHandler(int id);
        private Dictionary<String, RegisterEventHandler> registerEventMap = new Dictionary<string, RegisterEventHandler>();
        public RegisterController()
        {
            registerEventMap.Add(UserType.Expert.ToString(),(int id)=> {
                expert record = new expert();
                record.user_userId = id;
                var result = TableHandle<expert>().Create(record);
                SetSession(result.second, UserType.Expert);
            });
            registerEventMap.Add(UserType.Company.ToString(), (int id)=>{
                company record = new company();
                record.user_userId = id;
                var result = TableHandle<company>().Create(record);
                SetSession(result.second, UserType.Company);
            });
            registerEventMap.Add(UserType.Vendor.ToString(), (int id)=> {

                vendor record = new vendor();
                record.user_userId = id;
                var result = TableHandle<vendor>().Create(record);
                SetSession(result.second, UserType.Vendor);
            });

        }
        public ActionResult Index()
        {
            return View();
        }
        private SingleTableModule<T> TableHandle<T>()where T:class
        {
            return new SingleTableModule<T>();
        }
       
        private void SetSession(int userId,UserType type)
        {
            new Utility().SetSession(Session,userId, type);
        }
        
        
        private bool CheckUserType(String type)
        {
            return new Utility().CheckUserType(type);
        }
   
        [HttpPost]
        public void Register(user info)
        {
            if (ModelState.IsValid)
            {
                var table = new SingleTableModule<user>();
                var createResult = table.Create(info);
                if (createResult.first == true)
                {
                    var findIter = table.FindInfo(x => x.userId == createResult.second).SingleOrDefault();
                    Assert(findIter != null);
                    Assert(CheckUserType(findIter.user_type));
                    registerEventMap[findIter.user_type](findIter.userId);
                }
            }
            
        }


    }
}