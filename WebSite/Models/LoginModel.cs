using System.ComponentModel.DataAnnotations;
using WebSite.Controllers.Common;

namespace WebSite.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\w[\w\d_]{5,19}$", ErrorMessage = "Please enter valid name.")]
        public string name { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Please enter valid password")]
        public string password { get; set; }

        [Required(ErrorMessage = "*")]
        public UserType type { get; set; }
    }
}