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

    public partial class team
    {
        public team()
        {
            this.members = new HashSet<member>();
        }

        public int teamId { get; set; }
        [Required(ErrorMessage = "*")]

        public string team_name { get; set; }
        [Required(ErrorMessage = "*")]

        public string team_introduction { get; set; }
        public DateTime team_time { get; set; }
        public int purchaseId { get; set; }
        public int createId { get; set; }

        public virtual ICollection<member> members { get; set; }
        public virtual purchase purchase { get; set; }
    }
}
