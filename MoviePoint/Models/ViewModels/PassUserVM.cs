using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models.ViewModels
{
    public class PassUserVM
    {
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(12)]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(12)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[?!.#&^%$])(.\d).{6,}$", ErrorMessage = "Password sould contains at least 6 characters,at least one uppercase,at least one lowercase,at least one special character")]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
