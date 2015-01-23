using System.Collections.Generic;
using WebSite.Models;
namespace WebSite.Controllers.Module
{
    public class NewsModule
    {
        public List<news> GetPublishNewsInfoList(int companyId)
        {
            return new List<news> { };
        }

        public List<news> GetNewsList(int countMax)
        {
            return new List<news> { };
        }

        public bool PublishNew(news info)
        {
            return false;
        }

        public news NewsContent(int newsId)
        {
            return new news { };
        }

        public bool EditNews(news info)
        {
            return false;
        }


    }
}