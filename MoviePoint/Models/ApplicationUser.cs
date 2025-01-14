using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public CStatus? status { get; set; }
        public ICollection<Cart>? Carts { get; } = new List<Cart>();
    }
}
