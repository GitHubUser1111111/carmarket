using Carmarket.Domain.Users;

namespace Carmarket.Server.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterByPhone(string phone);

        Task<bool> RegisterByEmail(string email);

        Task<User> GetByPhone(string phone);

        Task<User> GetByEmail(string email);
    }
}
