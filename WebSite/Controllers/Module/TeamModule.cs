using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class TeamModule
    {
        private TeamDBContext db = new TeamDBContext();
        private MemberModule memberModule = new MemberModule();
        //public bool CreatVirtualTeam( team teamInfo, List<member> memberInfoList)
        //{
        //    return CreatEmptyTeam(teamInfo).first && memberModule.AddMember(memberInfoList);
        //}
        //public bool ApplyAddVirtualTeam(member info)
        //{
        //   return memberModule.AddMember(info);
        //}

        public Pair<bool, int> CreatEmptyTeam(team info)
        {
            Assert(info != null);
            var id = db.Teams.Add(info).teamId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }

        public bool DeleteTeam(team info)
        {
            if (memberModule.DeleteMemberByTeamId(info.teamId))
            {
                db.Teams.Remove(info);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public team GetTeamBaseInfo(int teamId)
        {
            return db.Teams.Find(teamId);
        }

        public Pair<team, List<member>> GetTeamInfo(int teamId)
        {
            var teamIter = GetTeamBaseInfo(teamId);
            if (teamIter != null)
            {
                return new Pair<team, List<member>>(teamIter, memberModule.FindMemberByTeamId(teamId));
            }
            return null;
        }

        public bool UpdateTeamBaseInfo(team record)
        {
            db.Entry<team>(record).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool UpdateTeamInfo(team record, List<member> members)
        {
            if (UpdateTeamBaseInfo(record))
            {
                if (memberModule.DeleteMemberByTeamId(record.teamId))
                {
                    return memberModule.AddMember(members);
                }
            }
            return false;
        }

        //展示团队列表的基本信息
        public List<team> GetTeamListBaseInfo(int countMax)
        {
            return db.Teams.Take(countMax).ToList();
        }

        //展示团队列表全部信息
        public List<Pair<team, List<member>>> GetTeamList(int countMax)
        {
            var result = new List<Pair<team, List<member>>>();
            foreach (var iter in GetTeamListBaseInfo(countMax))
            {
                result.Add(new Pair<team, List<member>>(iter, memberModule.FindMemberByTeamId(iter.teamId)));
            }
            return result;//db.Teams.Take(countMax).ToList();
        }

        //我加入的虚拟团队
        public List<Pair<team, List<member>>> GetUserAddVirtualTeamList(int vendorId)
        {
            var result = new List<Pair<team, List<member>>>();
            var query = from record in memberModule.db.Members where record.vendorId == vendorId group record by record.teamId into temp select temp.Key;
            foreach (var keyIter in query)
            {
                result.Add(GetTeamInfo(keyIter));
            }
            return result;
        }

        //我创建的虚拟团队
        public List<Pair<team, List<member>>> GetUserCreatedVirtualTeamList(int vendorId)
        {
            var result = new List<Pair<team, List<member>>>();
            var query = from record in db.Teams
                        where record.createId == vendorId
                        select record;
            foreach (var iter in query)
            {
                result.Add(new Pair<team, List<member>>(iter, memberModule.FindMemberByTeamId(iter.teamId)));
            }
            return result;
        }

        public team GetTeamBaseInfoByName(string name)
        {
            var query = from record in db.Teams where record.team_name == name select record;
            return query.SingleOrDefault();
        }
    }
}