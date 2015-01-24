using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebSite.Controllers.Common;

namespace WebSite.Models
{
    public class RegisterModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\W[\W\d_]{5-19}$", ErrorMessage = "Please enter valid name.")]
        [Remote("Verify", "CheckExpertNameRegister", ErrorMessage = "user name is registered")]

        public string name { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\s*d{11}\s*$", ErrorMessage = "Please enter valid phone no.")]
        public string telephone { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "email format error")]
        public string mail { get; set; }

        [Required(ErrorMessage = "*")]
        public string address { get; set; }

        public string introduce { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Please enter valid password")]
        public UserType type { get; set; }
        public string password { get; set; }
        public byte[] image { get; set; }
    }
}