
namespace SmartParking.API.Attributes
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        private const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]+$";
        private const int MinLength = 8;
        private const int MaxLength = 100;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Password is required");

            string password = value.ToString();

            if (password.Length < MinLength || password.Length > MaxLength)
                return new ValidationResult($"Password must be {MinLength}-{MaxLength} characters long");

            if (!System.Text.RegularExpressions.Regex.IsMatch(password, PasswordRegex))
                return new ValidationResult("Password must have 1 uppercase, 1 lowercase, 1 number, and 1 special character (!@#$%^&*)");

            return ValidationResult.Success;
        }
    }
}
