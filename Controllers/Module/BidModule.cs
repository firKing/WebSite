using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models;
using WebSite.Controllers.Common;
using System.Diagnostics.Contracts;
using System.Diagnostics.Debug;
using System;
namespace WebSite.Controllers.Module
{
    public class BidModule
    {
        private BidDBContext db = new BidDBContext();
        public bid GetBidInfo(int bidId)
        {
            return db.Bids.Find(bidId);
        }

        //获取发标者发布的标书列表
        public List<bid> GetPublishBidInfoList(int bidderId)
        {
            var query = from record in db.Bids where record.bidderId == bidderId select record;
            return query.ToList();
        }
        //获取某采购信息下所有标书
        public List<bid> GetPurchaseBidInfoList(int purchaseId)
        {
            var query = from record in db.Bids where record.purchaseId == purchaseId select record;
            return query.ToList();
        }
        public Pair<bool,int> CreateBid(bid info)
        {
            Assert(info != null);
            var id = db.Bids.Add(info).bidId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }

        public bool DeleteBid(int id)
        {
            var findResult = this.GetBidInfo(id);
            if (findResult != null)
            {
                db.Bids.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}