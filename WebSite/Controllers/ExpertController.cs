﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSite.Controllers.Module;
using WebSite.Models;
using System.Diagnostics.Debug;
using System.EnterpriseServices;
using System.Linq.Expressions;
using System.Web;
using WebSite.Controllers.Common;

namespace WebSite.Controllers
{
    public class ExpertController : Controller
    {
     

        // GET: ExpertHome
        //专家个人中心.基本信息
        public ActionResult Home()
        {
            return Info();
        }

        private bool CheckSession()
        {
            return Utility.CheckSession(UserType.Expert, Session);
        }
        private IQueryable<T> GetList<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return Utility.GetList<T>(expression);
        }
        private int GetSumCount<T, Tkey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, Tkey>> keySelector) where T : class
        {
            return Utility.GetSumCount(whereSelector, keySelector);
        }
        private IQueryable<T> GetList<T, TKey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return Utility.GetList<T, TKey>(page, count, whereSelector, keySelector);
        }
        public ActionResult Detail(int id)
        {
            var element = GetList<expert>(x=>x.expertId == id).SingleOrDefault();
            if (element != null)
            {
                ViewBag.name = element.user.user_name;
                ViewBag.phone = element.user.user_telephone;
                ViewBag.email = element.user.user_mail;
                ViewBag.address = element.user.user_address;
                ViewBag.content = element.user.user_introduction;
                return View("~/Views/Shared/userDetail.cshtml");
            }
            else
            {
                return HttpNotFound();
            }
        }
        private ActionResult Info()
        {
            if (CheckSession())
            {
                var sessionId = Convert.ToInt32(Session["user_id"]);
                var query = GetList<expert>(x => x.expertId == sessionId);
                var result = query.SingleOrDefault();
                Assert(result != null);
                ViewBag.home = result;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
        //企业邀请 列表
        public ActionResult InvitationList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;
                var sessionId = Convert.ToInt32(Session["user_id"]);
                ViewBag.list = GetList<invitation,int>(page,count,x => x.expertId== sessionId,x=>x.invitationId);
                ViewBag.sumPage = GetSumCount<invitation, int>(x => x.expertId == sessionId, x => x.expertId) / count + 1;
                ViewBag.pageNum = page;
                return View();
            }
            return RedirectToAction("Index", "Index");
        }
        //我发布的审核意见列表 
        public ActionResult AuditList(int page)
        {
            if (CheckSession())
            {
                const int count = 5;

                var sessionId = Convert.ToInt32(Session["user_id"]);
                ViewBag.list = GetList<audit,int>(page,count,x => x.expertId == sessionId,x=>x.auditId);
                ViewBag.sumPage = GetSumCount<audit, int>(x => x.expertId == sessionId, x => x.expertId) / count + 1;
                ViewBag.pageNum = page;
                return View();
            }
            return RedirectToAction("Index", "Index");

        }
     
    }
}