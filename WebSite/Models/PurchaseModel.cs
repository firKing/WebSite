using System;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class PurchaseModel
    {
        public purchase info { get; set; }

        public String invitees { get; set; }
        [Required(ErrorMessage = "*")]
        public String invitationContent { get; set; }
    }
}