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

    public partial class invitation
    {
        [Required(ErrorMessage = "*")]

        public int purchaseId { get; set; }
        public int expertId { get; set; }
        public int invitationId { get; set; }
        [Required(ErrorMessage = "*")]

        public string invitation_content { get; set; }

        public DateTime invitation_time { get; set; }

        public virtual expert expert { get; set; }
        public virtual purchase purchase { get; set; }
    }
}