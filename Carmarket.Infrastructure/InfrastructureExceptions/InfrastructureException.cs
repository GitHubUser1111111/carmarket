using Carmarket.Domain.Identity;
using Carmarket.Infrastructure.Resource;
using Carmarket.Infrastructure.Task;
using System.Diagnostics;

namespace Carmarket.Infrastructure.InfrastructureExceptions
{

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class InfrastructureException : System.Exception
    {
        private CarmarketIdentityUser user;
        private InfrastructureResource resource;
        private InfrastructureTask task;
        
        private string GetDebuggerDisplay()
        {
            return "InfrastructureException";
        }
    }
}
