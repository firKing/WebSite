using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class AuditModule
    {
        private AuditDBContext db = new AuditDBContext();

        public audit GetAuditInfo(int auditId)
        {
            return db.Audits.Find(auditId);
        }

        public List<audit> GetAuditList(int countMax)
        {
            return db.Audits.Take(countMax).ToList();
        }

        //获取企业发布的采购信息列表
        public List<audit> GetPublishAuditInfoList(int expertId)
        {
            var query = from record in db.Audits where record.expertId == expertId select record;
            return query.ToList();
        }

        public Pair<bool, int> CreateAudit(audit info)
        {
            Assert(info != null);
            var id = db.Audits.Add(info).auditId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }

        public bool DeleteAudit(int id)
        {
            var findResult = this.GetAuditInfo(id);
            if (findResult != null)
            {
                db.Audits.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}