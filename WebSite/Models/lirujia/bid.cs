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
    using System.ComponentModel.DataAnnotations;

    public partial class bid
    {
        public bid()
        {
            this.audits = new HashSet<audit>();
        }

        public int bidId { get; set; }
        [Required(ErrorMessage = "*")]
        public int purchaseId { get; set; }
        public int bidderId { get; set; }
        [Required(ErrorMessage = "*")]

        public string bid_title { get; set; }
        [Required(ErrorMessage = "*")]

        public string bid_device_name { get; set; }
        public System.DateTime bid_time { get; set; }
        [Required(ErrorMessage = "*")]

        public int bid_number { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(500, ErrorMessage = "Please enter introduction less 500 characters")]
        public string bid_introduction { get; set; }
        [Required(ErrorMessage = "*")]
        public string bid_content { get; set; }
        public virtual ICollection<audit> audits { get; set; }
        public virtual bidder bidder { get; set; }
        public virtual purchase purchase { get; set; }
    }
}