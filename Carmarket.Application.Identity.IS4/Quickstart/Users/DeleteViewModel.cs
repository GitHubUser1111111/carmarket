using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class DeleteViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
