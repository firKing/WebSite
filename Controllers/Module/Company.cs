using System.Collections.Generic;
using WebSite.Models;
namespace WebSite.Controllers.Module
{
    public class Company
    {
        public bool CreatedCompany(company record)
        {
            return false;
        }

        public bool DeleteCompany(int id)
        {
            return false;
        }

        public bool UpdateCompany(company record)
        {
            return false;
        }

        public List<company> ShowCompanyList(int countMax)
        {
            return new List<company> { };
        }

        public company GetCompanyInfo(int companyId)
        {
            return new company { };
        }

        public bool CompanyLogin(company info)
        {
            return false;
        }

        public bool CompanyRegister(company info)
        {
            return false;
        }
    }
}