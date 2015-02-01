using System;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebSite.Controllers.Module;
using WebSite.Models;

namespace WebSite.Controllers.Common
{
    public static class Utility
    {
        public static Pair<bool, bidder> CreateBidder(int tenderId, UserType type)
        {
            Assert(type == UserType.Vendor || type == UserType.Team);
            var record = new bidder();
            record.tendererId = tenderId;
            if (type == UserType.Team)
            {
                record.bidder_is_team = true;
            }
            else if (type == UserType.Vendor)
            {
                record.bidder_is_team = false;
            }
            var table = new SingleTableModule<bidder>();
            return table.Create(record);
        }

        public static String DateTimeToString(DateTime time)
        {
            return time.Year.ToString() + "-"
                  + time.Month.ToString() + "-"
                  + time.Day.ToString() + " "
                  + time.Hour.ToString() + "-"
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

        public static bool CheckSession(UserType type, HttpSessionStateBase session)
        {
            return Convert.ToInt32(session["user_id"]) != 0 &&
                 ((UserType)session["user_type"] == type);
        }

        public static IQueryable<T> GetList<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var table = new SingleTableModule<T>();
            var result = table.
            FindInfo(expression);
            Assert(result != null);
            return result;
        }

        public static bool CheckUserType(String type)
        {
            return type == UserType.Expert.ToString() ||
                      type == UserType.Vendor.ToString() ||
                      type == UserType.Company.ToString();
        }

        public static void SetSession(HttpSessionStateBase session, int userId, UserType type)
        {
            session["user_id"] = userId;
            session["user_type"] = type;
        }

        public static void ClearSession(HttpSessionStateBase session)
        {
            session["user_id"] = null;
            session["user_type"] = null;
        }

        private delegate void RegisterEventHandler(int id);

        public static void SetLoginSession(HttpSessionStateBase session, int userId, String type)
        {
            Dictionary<String, RegisterEventHandler> setSessionEventMap = new Dictionary<string, RegisterEventHandler>();
            setSessionEventMap.Add(UserType.Expert.ToString(), (int id) =>
             {
                 var result = new SingleTableModule<expert>().FindInfo(x => x.user_userId == id).SingleOrDefault();
                 Assert(result != null);
                 SetSession(session, result.expertId, UserType.Expert);
             });
            setSessionEventMap.Add(UserType.Company.ToString(), (int id) =>
            {
                var result = new SingleTableModule<company>().FindInfo(x => x.user_userId == id).SingleOrDefault();
                Assert(result != null);
                SetSession(session, result.companyId, UserType.Company);
            });
            setSessionEventMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                var result = new SingleTableModule<vendor>().FindInfo(x => x.user_userId == id).SingleOrDefault();
                Assert(result != null);
                SetSession(session, result.vendorId, UserType.Vendor);
            });
            setSessionEventMap[type](userId);
        }

        public static void RegisterUserTypeTable(int userId, String type)
        {
            var registerEventMap = new Dictionary<string, RegisterEventHandler>();
            registerEventMap.Add(UserType.Expert.ToString(), (int id) =>
            {
                expert record = new expert();
                record.user_userId = id;
                record.expert_accept_count = 0;
                var random = new Random();
                var number = random.Next(0, 5);
                record.expert_image = @"Protrait/" + number.ToString() + ".jpg";
                var result = new SingleTableModule<expert>().Create(record);
            });
            registerEventMap.Add(UserType.Company.ToString(), (int id) =>
            {
                company record = new company();
                record.user_userId = id;
                var result = new SingleTableModule<company>().Create(record);
            });

            registerEventMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                vendor record = new vendor();
                record.user_userId = id;
                var result = new SingleTableModule<vendor>().Create(record);
            });
            registerEventMap[type](userId);
        }

        public static IQueryable<T> GetList<T, TKey>(int page, int count, Expression<Func<T, TKey>> keySelector) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo().OrderByDescending(keySelector).Skip((page - 1) * count).Take(count);
            return container;
        }

        public static IQueryable<T> GetList<T, TKey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            var container = (new SingleTableModule<T>()).FindInfo(whereSelector).OrderByDescending(keySelector).Skip((page - 1) * count).Take(count);
            return container;
        }

        public static int GetSumCount<T, TKey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            var sum = (new SingleTableModule<T>()).FindInfo(whereSelector).OrderByDescending(keySelector).Count();
            return sum;
        }

        public static int GetSumCount<T, TKey>(Expression<Func<T, TKey>> keySelector) where T : class
        {
            var sum = GetSumCount(x => true, keySelector);
            return sum;
        }
    }
}