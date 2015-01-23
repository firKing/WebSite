using System.Collections.Generic;
using System.Linq;

using WebSite.Models;
namespace WebSite.Controllers.Module
{
    public class ExpertModule
    {
        private ExpertDBContext db = new ExpertDBContext();
        public expert GetExpertInfo(int expertId)
        {
            return db.Experts.Find(expertId);
        }

        public List<expert> GetExpertRecommendList(int countMax)
        {

            return null;
        }

        public bool ExpertLogin(expert info)
        {
            return false;
        }

        public bool ExpertRegister(expert info)
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