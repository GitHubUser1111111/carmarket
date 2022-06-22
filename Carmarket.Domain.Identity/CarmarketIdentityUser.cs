using Microsoft.AspNetCore.Identity;

namespace Carmarket.Domain.Identity
{
    public class CarmarketIdentityUser : IdentityUser
    {
        public string SubjectId { get; set; }
    }
}
