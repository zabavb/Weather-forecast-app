namespace UserAPI.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public User()
        {
            UserId = Guid.NewGuid();
            Username = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            Salt = string.Empty;
        }
    }
}
