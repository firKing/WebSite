using System.Diagnostics.Debug;
using WebSite.Controllers.Common;
using WebSite.Models;

namespace WebSite.Controllers.Module
{
    public class BidderModule
    {
        private BidderDBContext db = new BidderDBContext();

        private bidder GetBidderInfo(int bidderId)
        {
            return db.Bidders.Find(bidderId);
        }

        private bool BidderIsTeam(int bidderId)
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