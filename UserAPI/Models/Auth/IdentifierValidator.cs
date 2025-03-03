using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserAPI.Models.Auth
{
    public static class IdentifierValidator
    {
        public static ValidationResult? Validate(string identifier, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                return new ValidationResult("Username or Email is required.");

            if (new EmailAddressAttribute().IsValid(identifier) &&
                Regex.IsMatch(identifier, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                return ValidationResult.Success;

            if (new StringLengthAttribute(20) { MinimumLength = 4 }.IsValid(identifier))
                return ValidationResult.Success;

            return new ValidationResult("Username or Email must be a valid username or email address.");
        }
    }
}
