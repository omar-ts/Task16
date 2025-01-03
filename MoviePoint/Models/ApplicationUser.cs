using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "this field must not contain spaces")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "this field must not contain spaces")]
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public CStatus? status { get; set; }
        public ICollection<Cart> Carts { get; } = new List<Cart>();
    }
}
