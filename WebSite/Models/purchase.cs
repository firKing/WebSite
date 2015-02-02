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

    public partial class purchase
    {
        public purchase()
        {
            this.bids = new HashSet<bid>();
            this.invitations = new HashSet<invitation>();
            this.teams = new HashSet<team>();
        }

        public int purchaseId { get; set; }
        [Required(ErrorMessage = "*")]

        public int companyId { get; set; }
        [Required(ErrorMessage = "*")]

        public string purchase_title { get; set; }
        [Required(ErrorMessage = "*")]

        public string purchase_content { get; set; }
        public System.DateTime purchase_time { get; set; }
        public int hitId { get; set; }

        public virtual ICollection<bid> bids { get; set; }
        public virtual company company { get; set; }
        public virtual ICollection<invitation> invitations { get; set; }
        public virtual ICollection<team> teams { get; set; }
    }
}