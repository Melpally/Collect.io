using System.ComponentModel.DataAnnotations;

namespace Collect.io.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{8,}$",
                ErrorMessage = "Password should have at least 1 uppercase, 1 lowercase, 1 number and a special character and must be at least 8 characters long")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "Qwer!1234")
            {
                yield return new ValidationResult("Password is too simple", new[] { "Password" });
            }
        }

    }
}
