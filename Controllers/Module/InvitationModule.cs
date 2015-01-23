using System.Collections.Generic;
using WebSite.Models;
namespace WebSite.Controllers.Module
{
    public class InvitationModule
    {
        public List<invitation> GetCompanyInvitationList(int expertId)

        {
            return new List<invitation> { };
        }

        public List<invitation> GetPublishInvitationList(int companyId)

        {
            return new List<invitation> { };
        }

        public bool PublishExpertInvitation(  invitation info)
        {
            return false;
        }
}
}