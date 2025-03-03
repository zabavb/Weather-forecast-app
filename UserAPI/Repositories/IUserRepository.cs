using UserAPI.Models;
using UserAPI.Models.Auth;

namespace UserAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(Login request);
        Task<User?> GetUserByUsernameAsync(Login request);
        Task CreateAsync(User user);
    }
}
