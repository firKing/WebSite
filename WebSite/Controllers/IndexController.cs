using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using WebSite.Controllers.Common;
using System.Diagnostics.Debug;
namespace WebSite.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View(
                Tuple.Create
                (GetList<expert>(1),
                 GetList<news>(6),
                 GetList<purchase>(6),
                 GetList<team>(12)));
        }
        private IQueryable<T> GetList<T>(int countMax) where T :class
        {
            var container = (new SingleTableModule<T>()).FindInfo().Take(countMax);
            return container;
        }
        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            Assert(info!=null);
            if (ModelState.IsValid)
            {
                Assert(info.type != UserType.Team); 
                return RedirectToAction("Index", info.type.ToString()+"HomeController");
            }
            else
            {
                return View("Index");
            }

        }
    }
}