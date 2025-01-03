using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You should enter cinema name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should enter cinema description")]
        public string Description { get; set; }
        [ValidateNever]
        public string CinemaLogo { get; set; }
        [Required(ErrorMessage = "You should enter cinema address")]
        public string Address { get; set; }
        [ValidateNever]
        public ICollection<Movie>Movies { get;}=new List<Movie>();
    }
}
