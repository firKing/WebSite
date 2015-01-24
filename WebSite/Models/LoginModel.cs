using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Controllers.Common;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\W[\W\d_]{5-19}$", ErrorMessage = "Please enter valid name.")]
        public string name { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Please enter valid password")]
        public string password { get; set; }
        [Required(ErrorMessage = "*")]
        public UserType type { get; set; }
    }
}