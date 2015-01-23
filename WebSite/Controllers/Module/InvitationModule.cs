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
    public class InvitationModule
    {
        private InvitationDBContext db = new InvitationDBContext();
        public invitation GetInvitationInfo(int invitationId)
        {
            return db.Invitations.Find(invitationId);
        }

        public List<invitation> GetInvitationList(int countMax)
        {
            return db.Invitations.Take(countMax).ToList();
        }

        //获取企业发布的邀请信息列表
        public List<invitation> GetPublishInvitationInfoList(int purchaseId)
        {
            var query = from record in db.Invitations where record.purchaseId == purchaseId select record;
            return query.ToList();
        }

        public Pair<bool,int> CreateInvitation(invitation info)
        {
            Assert(info != null);
            var id = db.Invitations.Add(info).invitationId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }



        public bool DeleteInvitation(int id)
        {
            var findResult = this.GetInvitationInfo(id);
            if (findResult != null)
            {
                db.Invitations.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}