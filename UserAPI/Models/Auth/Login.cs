using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models.Auth
{
    public class Login
    {
        [Required(ErrorMessage = "Username or email is required.")]
        [CustomValidation(typeof(IdentifierValidator), nameof(IdentifierValidator.Validate))]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        public string Password { get; set; }

        public Login()
        {
            Identifier = string.Empty;
            Password = string.Empty;
        }
    }
}
