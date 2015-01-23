using WebSite.Models;
using System.Collections.Generic;

namespace WebSite.Controllers.Module
{
    public class BidModule
    {
        public bool PublishBid(bid info)
        {
            return false;
        }

        public bool EditBid(bid info)
        {
            return false;
        }

        public bid GetBidContent(int bidId)
        {
            return new bid { };
        }
        List<bid> GetPurchaseBidList(int purchaseId)
        {
            return new List<bid> { };
        }
        List<bid> GetPublishBidList(int vendorId)
        {
            return new List<bid> { };
        }
    }
}