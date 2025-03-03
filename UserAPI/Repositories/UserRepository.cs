using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using UserAPI.Data;
using UserAPI.Models;
using UserAPI.Models.Auth;

namespace UserAPI.Repositories
{
    public class UserRepository(UserDbContext context, IConnectionMultiplexer redis, ILogger<IUserRepository> logger) : IUserRepository
    {
        private readonly UserDbContext _context = context;
        private readonly IDatabase _redisDatabase = redis.GetDatabase();
        public readonly string _cacheKeyPrefix = "User_";
        public readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(10);
        private readonly ILogger<IUserRepository> _logger = logger;

        public async Task<User?> GetByIdAsync(Guid id)
        {
            string cacheKey = $"{_cacheKeyPrefix}{id}";
            string fieldKey = id.ToString();

            var cachedUser = await _redisDatabase.HashGetAsync(cacheKey, fieldKey);

            if (!cachedUser.IsNullOrEmpty)
            {
                _logger.LogInformation("Fetched from CACHE.");
                return JsonSerializer.Deserialize<User>(cachedUser!);
            }

            _logger.LogInformation("Fetched from DB.");
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _logger.LogInformation("Set to CACHE.");
                await _redisDatabase.HashSetAsync(
                    cacheKey,
                    fieldKey,
                    JsonSerializer.Serialize(user)
                );

                await _redisDatabase.KeyExpireAsync(cacheKey, _cacheExpiration);
            }

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(Login request)
        {
            var user = await _context.Users
                .AsNoTracking().FirstOrDefaultAsync(user => user.Email == request.Identifier);

            if (user == null)
                _logger.LogError($"User with email [{request.Identifier}] not found.");

            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(Login request)
        {
            var user = await _context.Users
                .AsNoTracking().FirstOrDefaultAsync(user => user.Username == request.Identifier);

            if (user == null)
                _logger.LogError($"User with phone number [{request.Identifier}] not found.");

            return user;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
