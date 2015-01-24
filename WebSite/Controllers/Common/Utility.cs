
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
        public bool CheckSession(UserType type, HttpSessionStateBase Session)
        {
            var typeConvertResult = Convert.ToString(Session["user_type"]);
            return !String.IsNullOrEmpty(typeConvertResult) && type.ToString() == typeConvertResult && !String.IsNullOrEmpty(Convert.ToString(Session["user_id"]));
        }
        public object GetList<T>(Func<T, int> expression, HttpSessionStateBase Session) where T : class
        {
   
            var table = new SingleTableModule<T>();
            var result = table.
            FindInfo(x => expression.Invoke(x) == Convert.ToInt32(Session["user_id"]));
            Assert(result != null);
            return result;
        }
    }
}