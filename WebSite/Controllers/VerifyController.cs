using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Module;
namespace WebSite.Controllers
{
    //验证逻辑的ajax服务器端.
    public class VerifyController : Controller
    {
        public ActionResult CheckExpertNameRegister(string name)
        {
            //TODO
            var expertModule = new ExpertModule();
            var result = expertModule.GetExpertInfoByName(name);
            if (result == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult CheckCompanyNameRegister(string name)
        {
            //TODO
            var expertModule = new ExpertModule();
            var result = expertModule.GetExpertInfoByName(name);
            if (result == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckVendorNameRegister(string name)
        {
            //TODO
            var vendorModule = new VendorModule();
            var result = vendorModule.GetVendorInfoByName(name);
            if (result == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckTeamNameRegister(string name)
        {
            //TODO
            var teamModule = new TeamModule();
            var result = teamModule.GetTeamBaseInfoByName(name);
            if (result == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        
        // GET: Verify
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}