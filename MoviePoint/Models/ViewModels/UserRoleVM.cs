using Microsoft.AspNetCore.Identity;

namespace MoviePoint.Models.ViewModels
{
    public class UserRoleVM
    {
        public ApplicationUser AppUser { get; set; }
        public List<IdentityRole> Role { get; set; }=new List<IdentityRole>();
    }
}
