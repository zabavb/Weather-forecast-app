namespace UserAPI.Models
{
    public static class PasswordHelper
    {
        public static string GenerateSalt() => BCrypt.Net.BCrypt.GenerateSalt();

        public static string HashPassword(string password, string salt) =>
            BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

}
