using System.Collections.Generic;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class PurchaseModule
    {
        public List<purchase> GetPublishPurchaseInfoList(int companyId)
        {
            return new List<purchase> { };
        }

        public List<purchase> GetPurchaseInfoList(int countMax)
        {
            return new List<purchase> { };
        }

        public bool PublishPurchase(purchase info)
        {
            return false;
        }

        public bool EditPurchase(purchase info)
        {
            return false;
        }

        public purchase GetPurchaseContent(int purchaseId)
        {
            return new purchase { };
        }

        public bool DeletePurchase(int purchaseId)
        {
            return false;
        }
    }
}