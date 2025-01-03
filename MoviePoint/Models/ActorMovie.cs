using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class ActorMovie
    {
        [Required(ErrorMessage ="You should enter movie name")]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "You should enter actor name")]
        public int ActorId { get; set; }
        [ValidateNever]
        public Movie Movie { get; set; }
        [ValidateNever]
        public Actor Actor { get; set; }
    }
}
