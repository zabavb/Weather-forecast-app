using UserAPI.Models;
using UserAPI.Models.Auth;

namespace UserAPI.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> AuthenticateAsync(Login request);
        Task<User> RegisterAsync(Register entity);
    }
}
