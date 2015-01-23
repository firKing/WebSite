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
    public class BidderModule
    {
        private BidderDBContext db = new BidderDBContext();

        bidder GetBidderInfo(int bidderId)
        {
            return db.Bidders.Find(bidderId);
        }
        bool BidderIsTeam(int bidderId)
        {
            return GetBidderInfo(bidderId).bidder_is_team;
        }
        public Pair<bool, int> CreateBidder(bidder info)
        {
            Assert(info != null);
            var id = db.Bidders.Add(info).bidderId;
            return new Pair<bool, int>(db.SaveChanges() > 0, id);
        }
        public bool DeleteBidder(int id)
        {
            var findResult = this.GetBidderInfo(id);
            if (findResult != null)
            {
                db.Bidders.Remove(findResult);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}