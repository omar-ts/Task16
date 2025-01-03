using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models.ViewModels
{
    public class LoginVM
    {
        public int Id { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
