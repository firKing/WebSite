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
    using System.Web.Mvc;

    public partial class user
    {
        public int userId { get; set; }
        public string user_type { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\W[\W\d_]{5-19}$", ErrorMessage = "Please enter valid name.")]
        [Remote("Verify", "CheckuserNameRegister", ErrorMessage = "user name is registered")]

        public string user_name { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\s*d{11}\s*$", ErrorMessage = "Please enter valid phone no.")]
        public string user_telephone { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "email format error")]
        public string user_mail { get; set; }

        [Required(ErrorMessage = "*")]
        public string user_address { get; set; }

        public string user_introduce { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Please enter valid password")]
        public string user_password { get; set; }

        public virtual expert expert { get; set; }
        public virtual company company { get; set; }
        public virtual vendor vendor { get; set; }
    }
}
