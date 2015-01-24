using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models;
using WebSite.Controllers.Common;
using System.Diagnostics.Debug;
//数据库那边处理模型验证是否为空等等
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
            return db.Experts.Take(countMax).ToList();
        }

        public bool ExpertLogin(expert info)
        {
            Assert(info != null);
            var query = from record in db.Experts
                        where record.expert_name == info.expert_name
                        select new {name = record.expert_name,id = record.expertId };
            var result = query.Single();
            if(result == null)
            {
                return false;
            }
            else
            {
                HttpContext.Current.Session["user_name"] =result.name;
                HttpContext.Current.Session["user_id"] = result.id;
                HttpContext.Current.Session["user_type"] = UserType.Expert;
                return true;
            }
        }
       
        public Pair<bool,int> ExpertRegister(expert info)
        {
            Assert(info != null);
            var id = db.Experts.Add(info).expertId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }

        

        public bool DeleteExpert(int id)
        {
            var findResult = this.GetExpertInfo(id);
            if(findResult!=null)
            {
               db.Experts.Remove(findResult);
               return db.SaveChanges()>0;
            }
            return false;
        }

        public bool UpdateExpert(expert record)
        {
            db.Entry<expert>(record).State = System.Data.Entity.EntityState.Modified;

            return db.SaveChanges()>0;
        }

        public List<expert> ShowExpertList(int countMax)
        {

            return db.Experts.Take(countMax).ToList() ;
        }
    }
}