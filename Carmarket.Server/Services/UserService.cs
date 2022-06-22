using Carmarket.Domain.Users;
using Carmarket.Server.Interfaces;

namespace Carmarket.Server.Services
{
    public class UserService : IUserService
    {
        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterByPhone(string phone)
        {
            throw new NotImplementedException();
        }
    }
}
