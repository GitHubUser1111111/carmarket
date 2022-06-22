using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string SubjectId { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
