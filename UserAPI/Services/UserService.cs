using UserAPI.Models;
using UserAPI.Models.Auth;
using UserAPI.Repositories;

namespace UserAPI.Services
{
    public class UserService(IUserRepository repository, ILogger<IUserService> logger) : IUserService
    {
        private readonly IUserRepository _repository = repository;
        private readonly ILogger<IUserService> _logger = logger;
        private string _message = string.Empty;

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                _message = $"User with ID [{id}] not found.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            _logger.LogInformation("User with ID [{id}] successfully fetched.", id);

            return user;
        }

        public async Task<User?> AuthenticateAsync(Login request)
        {
            User? user = request.Identifier.Contains('@') ?
                await _repository.GetUserByEmailAsync(request) :
                await _repository.GetUserByUsernameAsync(request);

            return await Task.FromResult(IsRightPassword(user, request.Password) ? user : null);
        }

        public async Task<User> RegisterAsync(Register request)
        {
            if (request == null)
            {
                _message = "User was not provided for creation.";
                _logger.LogError(_message);
                throw new ArgumentNullException(null, _message);
            }

            var salt = PasswordHelper.GenerateSalt();
            User user = new()
            {
                UserId = new Guid(),
                Username = request.Username,
                Email = request.Email,
                Salt = salt,
                PasswordHash = PasswordHelper.HashPassword(request.Password, salt)
            };

            try
            {
                await _repository.CreateAsync(user);
                _message = "Successful user registration in UserAPI.Services.AuthService.RegisterAsync";
                _logger.LogInformation(_message);

                return user;
            }
            catch (Exception ex)
            {
                _message = $"Error occurred while registering new user.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }

        private bool IsRightPassword(User? user, string password)
        {
            try
            {
                if (user != null && !string.IsNullOrWhiteSpace(password))
                {
                    string hashedPassword = PasswordHelper.HashPassword(password, user.Salt);
                    return hashedPassword == user.PasswordHash;
                }
            }
            catch
            {
                _message = $"Error occurred while registering new user.";
                _logger.LogError(_message);
            }

            return false;
        }
    }
}
