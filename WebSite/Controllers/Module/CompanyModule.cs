using System.Collections.Generic;
using System.Diagnostics.Debug;
using System.Linq;
using System.Web;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class CompanyModule
    {
        private CompanyDBContext db = new CompanyDBContext();

        public company GetCompanyInfo(int companyId)
        {
            return db.Companies.Find(companyId);
        }

        public bool CompanyLogin(company info)
        {
            Assert(info != null);
            var query = from record in db.Companies
                        where record.company_name == info.company_name
                        select new { name = record.company_name, id = record.companyId };
            var result = query.SingleOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                HttpContext.Current.Session["user_name"] = result.name;
                HttpContext.Current.Session["user_id"] = result.id;
                HttpContext.Current.Session["user_type"] = UserType.Company;
                return true;
            }
        }

        public Pair<bool, int> CompanyRegister(company info)
        {
            Assert(info != null);
            var id = db.Companies.Add(info).companyId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }

        public bool DeleteCompany(int id)
        {
            var findResult = this.GetCompanyInfo(id);
            if (findResult != null)
            {
                db.Companies.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool UpdateCompany(company record)
        {
            db.Entry<company>(record).State = System.Data.Entity.EntityState.Modified;

            return db.SaveChanges() > 0;
        }

        public List<company> GetCompanyList(int countMax)
        {
            return db.Companies.Take(countMax).ToList();
        }

        public company GetCompanyInfoByName(string name)
        {
            var query = from record in db.Companies where record.company_name == name select record;
            return query.SingleOrDefault();
        }
    }
}