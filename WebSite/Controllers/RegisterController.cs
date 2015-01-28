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
        [HttpPost]
        public void Register(user info)
        {
            if (ModelState.IsValid)
            {
                var table = new SingleTableModule<user>();
                table.Create(info);
            }
            
        }


    }
}