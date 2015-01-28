
using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace WebSite.Controllers.Common
{
    public class Utility
    {
        public UserType GetUsetTypeByString(String type)
        {
            if (type == "Expert")
            {
                return UserType.Expert;
            }
            else if (type == "Company")
            {
                return UserType.Company;
            }
            else if (type == "Vendor")
            {
                return UserType.Vendor;
            }
            else if (type == "Team")
            {
                return UserType.Team;
            }
            else
            {
                Assert(false);
            }
            return UserType.Team;



        }
        public bool CheckSession(UserType type, HttpSessionStateBase Session)
        {
            return Convert.ToInt32(Session["user_id"]) != 0 &&
                 ((UserType)Session["user_type"] == UserType.Expert
                 || (UserType)Session["user_type"] == UserType.Vendor
                || (UserType)Session["user_type"] == UserType.Company
                );
        }
        public object GetList<T>(Func<T, int> expression, HttpSessionStateBase Session) where T : class
        {
   
            var table = new SingleTableModule<T>();
            var result = table.
            FindInfo(x => expression.Invoke(x) == Convert.ToInt32(Session["user_id"]));
            Assert(result != null);
            return result;
        }
        public bool CheckUserType(String type)
        {
            return type == UserType.Expert.ToString() ||
                      type == UserType.Vendor.ToString() ||
                      type == UserType.Company.ToString();
        }
        public void SetSession(HttpSessionStateBase Session,int userId, UserType type)
        {
            Session["user_id"] = userId;
            Session["user_type"] = type;
        }
        private delegate void RegisterEventHandler(int id);
        private SingleTableModule<T> TableHandle<T>() where T : class
        {
            return new SingleTableModule<T>();
        }

        public void SetLoginSession(HttpSessionStateBase Session, int userId,String type)
        {
            Dictionary<String, RegisterEventHandler> registerEventMap = new Dictionary<string, RegisterEventHandler>();
            registerEventMap.Add(UserType.Expert.ToString(), (int id) =>
            {
                expert record = new expert();
                record.user_userId = id;
                var result = TableHandle<expert>().Create(record);
                SetSession(Session,result.second, UserType.Expert);
            });
            registerEventMap.Add(UserType.Company.ToString(), (int id) =>
            {
                company record = new company();
                record.user_userId = id;
                var result = TableHandle<company>().Create(record);
                SetSession(Session,result.second, UserType.Company);
            });
            
            registerEventMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                 vendor record = new vendor();
                record.user_userId = id;
                var result = TableHandle<vendor>().Create(record);
                SetSession(Session,result.second, UserType.Vendor);
            });
            registerEventMap[type](userId);
        }
    }
}