using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models.ViewModels
{
    public class CinRegistVM
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
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[?!.#&^%$])(.\d).{6,}$", ErrorMessage = "Password sould contains at least 6 characters,at least one uppercase,at least one lowercase,at least one special character")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [ValidateNever]
        public string CinemaLogo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Detail { get; set; }
    }
}
