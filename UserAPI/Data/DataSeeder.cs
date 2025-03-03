using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {

            var salt1 = PasswordHelper.GenerateSalt();
            var hash1 = PasswordHelper.HashPassword("123456", salt1);
            var user1 = new User
            {
                UserId = Guid.NewGuid(),
                Username = "john.doe",
                Email = "john.doe@example.com",
                PasswordHash = hash1,
                Salt = salt1
            };

            var salt2 = PasswordHelper.GenerateSalt();
            var hash2 = PasswordHelper.HashPassword("654321", salt2);
            var user2 = new User
            {
                UserId = Guid.NewGuid(),
                Username = "jane.smith",
                Email = "jane.smith@example.com",
                PasswordHash = hash2,
                Salt = salt2
            };

            modelBuilder.Entity<User>().HasData(user1, user2);
        }
    }
}
