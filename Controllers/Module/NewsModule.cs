using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models;
using WebSite.Controllers.Common;
using System.Diagnostics.Contracts;
using System.Diagnostics.Debug;
using System;
namespace WebSite.Controllers.Module
{
    public class NewsModule
    {
        private NewsDBContext db = new NewsDBContext();
        public news GetNewsInfo(int newsId)
        {
            return db.Newses.Find(newsId);
        }

        public List<news> GetNewsList(int countMax)
        {
            return db.Newses.Take(countMax).ToList();
        }

        //获取企业发布的新闻列表
        public List<news> GetPublishNewsInfoList(int companyId)
        {
            var query = from record in db.Newses where record.companyId == companyId select record;
            return query.ToList();
        }

        public Pair<bool,int> CreateNews(news info)
        {
            Assert(info != null);
            var id = db.Newses.Add(info).newsId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }



        public bool DeleteNews(int id)
        {
            var findResult = this.GetNewsInfo(id);
            if (findResult != null)
            {
                db.Newses.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool EditNews(news record)
        {
            db.Entry<news>(record).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;
        }


    }
}