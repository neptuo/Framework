using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Models
{
    class RegisterUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordAgain { get; set; }

        [Required]
        public int? Age { get; set; }

        public ICollection<int> RoleIDs { get; }

        public RegisterUserModel() => RoleIDs = new List<int>();
    }
}
