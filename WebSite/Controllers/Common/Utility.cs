
using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using System.Linq.Expressions;
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
    }
}