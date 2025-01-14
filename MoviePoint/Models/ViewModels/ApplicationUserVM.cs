using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models.ViewModels
{
    public class ApplicationUserVM
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "UserName must not contain spaces.")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(12)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[?!.#&^%$])(.\d).{6,}$",ErrorMessage ="Password sould contains at least 6 characters,at least one uppercase,at least one lowercase,at least one special character")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "First name must not contain spaces.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "Last name must not contain spaces.")]
        public string LastName { get; set; }
        [ValidateNever]
        public string ProfilePicture { get; set; }

        public List<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();
    }
}
