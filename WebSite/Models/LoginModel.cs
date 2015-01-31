using System.ComponentModel.DataAnnotations;
using WebSite.Controllers.Common;

namespace WebSite.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*")]
        public string name { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Please enter valid password")]
        public string password { get; set; }

        [Required(ErrorMessage = "*")]
        public UserType type { get; set; }
    }
}