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
    public class MemberModule
    {
        public MemberDBContext db = new MemberDBContext();
        public bool AddMember(List<member> members)
        {
            foreach (var iter in members)
            {
                db.Members.Add(iter);
            }
            return db.SaveChanges() > 0;
        }
        public bool AddMember(member target)
        {
            db.Members.Add(target);
            return db.SaveChanges() > 0;
        }

        public bool DeleteMemberByTeamId(int teamId)
        {
            var findIter = FindMemberByTeamId(teamId);
            if (findIter != null)
            {
                foreach(var iter in findIter)
                {
                    DeleteMember(iter);
                }
                return db.SaveChanges() > 0;
            }
            else
            {
                return false;
            }

        }
        public bool DeleteMember(member info)
        {
            db.Members.Remove(info);
            return  db.SaveChanges() > 0;
        }
       
        public List<member> FindMemberByTeamId(int teamId)
        {
            var query = from record in db.Members where record.teamId == teamId select record;
            return query.ToList();
        }
        public member FindMemberBymemberId(int memberId)
        {
            return db.Members.Find(memberId);
        }
        
        public Pair<bool,int> CreateMember(member info)
        {
            var id = db.Members.Add(info).memberId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);

        }

    }
}