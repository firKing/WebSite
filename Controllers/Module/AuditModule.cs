using System.Collections.Generic;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class AuditModule
    {
        public List<audit> GetPublishExpertAuditList(int expertId)
        {
            return new List<audit> { };
        }

        public bool PublishAudit(audit info)
        {
            return false;
        }

        public List<audit> GetBidAuditList(int bidId)
        {
            return new List<audit> { };
        }
    }
}