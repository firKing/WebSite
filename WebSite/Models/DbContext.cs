using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WebSite.Models
{
    public class WebSiteDBContext<T> : System.Data.Entity.DbContext where T : class
    {
        public System.Data.Entity.DbSet<T> table { get; set; }
        public WebSiteDBContext()
           : base("name=WebSiteEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }

    public class AdminDbContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<admin> admins { get; set; }
        public AdminDbContext()
           : base("name=WebSiteEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }

    public class AuditDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<audit> Audits { get; set; }
    }

    public class BidDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<bid> Bids { get; set; }
    }

    public class BidderDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<bidder> Bidders { get; set; }
    }

    public class CompanyDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<company> Companies { get; set; }
    }

    public class ExpertDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<expert> Experts { get; set; }
    }

    public class InvitationDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<invitation> Invitations { get; set; }
    }

    public class MemberDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<member> Members { get; set; }
    }

    public class NewsDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<news> Newses { get; set; }

        public System.Data.Entity.DbSet<WebSite.Models.company> companies { get; set; }
    }

    public class PurchaseDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<purchase> Purchases { get; set; }
    }

    public class TeamDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<team> Teams { get; set; }
    }

    public class VendorDBContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<vendor> Vendors { get; set; }
    }
}