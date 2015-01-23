using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Controllers.Common;
using WebSite.Models;
namespace WebSite.Controllers.Module
{
    public class TeamModule
    {
        public List<team> GetVirtualTeamList(int countMax)
        {
            return new List<team> { };
        }
        public bool CreatVirtualTeam( team teamInfo, List<member> memberInfoList)
        {
            return false;
        }
        public bool AddVirtualTeam(int teamId)
        {
            return false;
        }
        public bool DeleteVirtualTeam(int teamId)
        {
            return false;
        }
        public bool CreatedTeam(team record)
        {
            return false;
        }
        public bool DeleteTeam(int id)
        {
            return false;
        }
        public bool UpdateTeam(team record)
        {
            return false;
        }
        public List<team> ShowTeamList(int countMax)
        {
            return new List<team> { };
        }
        public Pair<team, List<member>> GetAddVirtualTeamList(int vendorId)
        {
            return new Pair<team, List<member>> { };
        }
        public Pair<team, List<member>> GetCreatedVirtualTeamList(int vendorId)
        {
            return new Pair<team, List<member>> { };
        }

    }
}