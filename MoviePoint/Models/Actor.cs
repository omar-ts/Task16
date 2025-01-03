using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You should enter actor name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should enter actor description")]
        public string Bio { get; set; }
        [ValidateNever]
        public string ProfilePicture { get; set; }
        [Required(ErrorMessage = "You should enter actor news url")]
        public string News { get; set; }
        [ValidateNever]
        public ICollection<ActorMovie> ActorMovies { get; }=new List<ActorMovie>();
    }
}
