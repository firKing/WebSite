using System.Collections.Generic;
using WebSite.Models;
namespace WebSite.Controllers.Module
{
    public class ExpertModule
    {
        public expert GetExpertInfo(int expertId)
        {
            return new expert { };
        }

        public List<expert> GetExpertRecommendList(int countMax)
        {
            return new List<expert> { };
        }

        public bool ExpertLogin(expert info)
        {
            return false;
        }

        public bool ExpertRegister(expert info)
        {
            return false;
        }

        public bool CreatedExpert(expert record)
        {
            return false;
        }

        public bool DeleteExpert(int id)
        {
            return false;
        }

        public bool UpdateExpert(expert record)
        {
            return false;
        }

        public List<expert> ShowExpertList(int countMax)
        {
            return new List<expert> { };
        }
    }
}