
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
    public static class Utility
    {
        public static String DateTimeToString(DateTime time)
        {
          return time.Year.ToString() + "-"
                + time.Month.ToString() + "-" 
                + time.Day.ToString() + " "
                + time.Hour.ToString()+"-" 
                + time.Minute.ToString() + "-"
                + time.Second.ToString();
        }


        public static UserType GetUsetTypeByString(String type)
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
        public static bool CheckSession(UserType type, HttpSessionStateBase Session)
        {
            return Convert.ToInt32(Session["user_id"]) != 0 &&
                 ((UserType)Session["user_type"] == UserType.Expert
                 || (UserType)Session["user_type"] == UserType.Vendor
                || (UserType)Session["user_type"] == UserType.Company
                );
        }
        public static object GetList<T>(Func<T, int> expression, HttpSessionStateBase Session) where T : class
        {
            var table = new SingleTableModule<T>();
            var result = table.
            FindInfo(x => expression.Invoke(x) == Convert.ToInt32(Session["user_id"]));
            Assert(result != null);
            return result;
        }
        public static bool CheckUserType(String type)
        {
            return type == UserType.Expert.ToString() ||
                      type == UserType.Vendor.ToString() ||
                      type == UserType.Company.ToString();
        }
        public static void SetSession(HttpSessionStateBase Session,int userId, UserType type)
        {
            Session["user_id"] = userId;
            Session["user_type"] = type;
        }
        private delegate void RegisterEventHandler(int id);
      

        public static void SetLoginSession(HttpSessionStateBase Session, int userId,String type)
        {
            Dictionary<String, RegisterEventHandler> registerEventMap = new Dictionary<string, RegisterEventHandler>();
            registerEventMap.Add(UserType.Expert.ToString(), (int id) =>
            {
                expert record = new expert();
                record.user_userId = id;
                var result = new SingleTableModule< expert>().Create(record);
                SetSession(Session,result.second.expertId, UserType.Expert);
            });
            registerEventMap.Add(UserType.Company.ToString(), (int id) =>
            {
                company record = new company();
                record.user_userId = id;
                var result = new SingleTableModule< company>().Create(record);
                SetSession(Session,result.second.companyId, UserType.Company);
            });
            
            registerEventMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                 vendor record = new vendor();
                record.user_userId = id;
                var result = new SingleTableModule<vendor>().Create(record);
                SetSession(Session,result.second.vendorId, UserType.Vendor);
            });
            registerEventMap[type](userId);
        }
        public static IQueryable<T> GetList<T, Tkey>(int page, int count, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().OrderByDescending(keySelector).Skip((page - 1) * count).Take(count);
            return container;
        }
        public static IQueryable<T> GetList<T, Tkey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {

            var container = (new SingleTableModule<T>()).FindInfo(whereSelector).OrderByDescending(keySelector).Skip((page-1) * count).Take(count);
            return container;
        }
        public static int GetSumCount<T, Tkey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            var sum = (new SingleTableModule<T>()).FindInfo(whereSelector).OrderByDescending(keySelector).Count();
            return sum;
        }
        public static int GetSumCount<T, Tkey>( Expression<Func<T, Tkey>> keySelector) where T : class
        {
            var sum = GetSumCount(x=>true,keySelector);
            return sum;
        }
    }
}