﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
            var result = GetSingleTableRecord<bidder>(x => x.tendererId == record.tendererId && x.bidder_is_team == record.bidder_is_team);
            if (result == null)
            {
                return CreateRecord<bidder>(record);
            }
            return new Pair<bool, bidder>(true, result);
        }

        public static String DateTimeToString(DateTime time)
        {
            return time.ToString("yyyy/dd/mm hh:mm");
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

        public static IQueryable<T> GetList<T>(int countMax) where T : class
        {
            return GetList<T>(x => true).Take(countMax);
        }

        public static IQueryable<T> GetList<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var result = new SingleTableModule<T>().FindInfo(expression);
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

        public static Pair<bool, T> CreateRecord<T>(T record) where T : class
        {
            return new SingleTableModule<T>().Create(record);
        }

        public static void SetLoginSession(HttpSessionStateBase session, int userId, String type)
        {
            Dictionary<String, RegisterEventHandler> setSessionEventMap = new Dictionary<string, RegisterEventHandler>();
            setSessionEventMap.Add(UserType.Expert.ToString(), (int id) =>
             {
                 var result = GetSingleTableRecord<expert>(x => x.user_userId == id);
                 Assert(result != null);
                 SetSession(session, result.expertId, UserType.Expert);
             });
            setSessionEventMap.Add(UserType.Company.ToString(), (int id) =>
            {
                var result = GetSingleTableRecord<company>(x => x.user_userId == id);
                Assert(result != null);
                SetSession(session, result.companyId, UserType.Company);
            });
            setSessionEventMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                var result = GetSingleTableRecord<vendor>(x => x.user_userId == id);
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
                var random = new Random();
                var number = random.Next(1, 5);
                var imageUrl = @"Protrait/" + number.ToString() + ".jpg";
                CreateRecord<expert>(new expert { user_userId = id, expert_image = imageUrl });
            });
            registerEventMap.Add(UserType.Company.ToString(), (int id) =>
            {
                CreateRecord<company>(new company { user_userId = id });
            });

            registerEventMap.Add(UserType.Vendor.ToString(), (int id) =>
            {
                CreateRecord<vendor>(new vendor { user_userId = id });
            });
            registerEventMap[type](userId);
        }

        public static IQueryable<T> GetList<T, TKey>(int page, int count, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return GetList<T, TKey>(page, count, x => true, keySelector);
        }

        public static IQueryable<T> GetList<T, TKey>(int page, int count, Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return GetList<T>(whereSelector).OrderByDescending(keySelector).Skip((page - 1) * count).Take(count);
        }

        public static int GetSumCount<T, TKey>(Expression<Func<T, bool>> whereSelector, Expression<Func<T, TKey>> keySelector) where T : class
        {
            return GetList<T>(whereSelector).OrderByDescending(keySelector).Count();
        }

        public static int GetSumCount<T, TKey>(Expression<Func<T, TKey>> keySelector) where T : class
        {
            return GetSumCount(x => true, keySelector);
        }

        public class BidRecordInfo
        {
            public String name { get; set; }
            public int staute { get; set; }
            public String introduction { get; set; }
        }

        public static BidRecordInfo GetBidUser(bidder info,bid bidInfo)
        {
            if (info.bidder_is_team)
            {
                var result = GetSingleTableRecord<team>(x => x.teamId == info.tendererId);
                Assert(result != null);
                return new BidRecordInfo
                {
                    name = result.team_name,
                    introduction = result.team_introduction,
                    staute = BidStatus(bidInfo.purchaseId,bidInfo.bidId)
                };
            }
            else
            {
                var result = GetSingleTableRecord<vendor>(x => x.vendorId == info.tendererId);
                Assert(result != null);
                return new BidRecordInfo
                {
                    name = result.user.user_name,
                    introduction = result.user.user_introduction,
                    staute = BidStatus(bidInfo.purchaseId, bidInfo.bidId)
                };
            }
        }
        //0审核中 1 选中 2落选
        private static int BidStatus(int purchaseId, int bidId)
        {
            var element = Utility.GetSingleTableRecord<purchase>(x => x.purchaseId == purchaseId);
            Assert(element != null);
            if (element.hitId == 0)
            {
                return 0;
            }
            else if (element.hitId == bidId)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public static String UploadFileGetUrl(bid info, HttpRequestBase request, String uploadName)
        {
            // uploadName = "bid_content";
            Assert(request.Files[uploadName] != null);

            var file = request.Files[uploadName];
            Assert(request.Files.Count == 1);
            string path = "/Uploads/";
            string fileName = Path.GetFileName(file.FileName);
            Regex regex = new Regex(@"\.(pdf|txt|ppt|doc|wps|xls)");
            Assert(fileName != null);
            if (!regex.IsMatch(fileName))
            {
                Assert(false);
            }
            path = Path.Combine(path, fileName);
            var fullPath = AppDomain.CurrentDomain.BaseDirectory + path;
            file.SaveAs(fullPath);
            return path;
        }

        public static Pair<bool, T> EditRecord<T>(Expression<Func<T, bool>> whereSelector, Func<T, T> infoFunctor) where T : class
        {
            var table = new SingleTableModule<T>();
            return table.Edit(whereSelector, infoFunctor);
        }

        public static T GetSingleTableRecord<T>(Expression<Func<T, bool>> whereSelector) where T : class
        {
            var iter = GetList<T>(whereSelector).SingleOrDefault();
            Assert(iter != null);
            return iter;
        }

        public static void FillBidRecord(bid info, bidder bidderInfo, HttpRequestBase request, String uploadName)
        {
            info.bidderId = bidderInfo.bidderId;
            info.bid_content = UploadFileGetUrl(info, request, uploadName);
            info.bid_time = DateTime.Now;
        }


        public static String Md5(String password)
        {
            return password;
            //const String salt = "ZGF-DTC-LRJ-XYP-ZYX233333";
            //Assert(password != null);
            //byte[] result = Encoding.Default.GetBytes(salt + password.Trim());    //tbPass为输入密码的文本框
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] output = md5.ComputeHash(result);
            //return BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
        }


        public static String HtmlDecode(String target)
        {
            target = HttpUtility.HtmlDecode(target);
            return target.Replace("&nbsp;", " ");
        }
     
    }
}