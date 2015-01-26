using System;
using System.Diagnostics.Debug;
using System.Linq;
using System.Linq.Expressions;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class SingleTableModule<T> where T : class
    {
        private WebSiteDBContext<T> db = new WebSiteDBContext<T>();
        // exmaple : FindInfo(x=>x.filed == target);

        public IQueryable<T> FindInfo(Expression<Func<T, bool>> predictor)
        {
            var query = db.table.Where(predictor);
            return query;
        }

        public IQueryable<T> FindInfo()
        {
            return db.table;
        }

        public IQueryable<R> FindInfo<R>(Expression<Func<T, bool>> predictor, Expression<Func<T, R>> selector) where R : class
        {
            var query = db.table.Where(predictor).Select(selector);
            return query;
        }

        public Pair<bool, int> Create(T info)
        {
            Assert(info != null);
            var addResult = db.table.Add(info);
            var tableType = db.table.GetType();
            var propertyInfo = tableType.GetProperty(tableType.ToString() + "_id");
            var id = (int)propertyInfo.GetValue(addResult);
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }

        public bool Edit(T info)
        {
            db.Entry<T>(info).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Delete(T info)
        {
            db.table.Remove(info);
            return db.SaveChanges() > 0;
        }

        public bool Delete(Expression<Func<T, bool>> predictor)
        {
            var resultList = FindInfo(predictor);
            if (resultList != null)
            {
                foreach (var iter in resultList)
                {
                    db.table.Remove(iter);
                }
                return db.SaveChanges() > 0;
            }
            return false;
        }
        public int GetRecordId(T record)
        {
            var tableType = db.table.GetType();
            var propertyInfo = tableType.GetProperty(tableType.ToString() + "_id");
            return (int)propertyInfo.GetValue(record);
        }
    }
}