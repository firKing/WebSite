//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bidder
    {
        public bidder()
        {
            this.bids = new HashSet<bid>();
        }
    
        public int bidderId { get; set; }
        public bool bidder_is_team { get; set; }
        public int tendererId { get; set; }
    
        public virtual ICollection<bid> bids { get; set; }
    }
}
