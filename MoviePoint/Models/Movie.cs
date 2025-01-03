using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You should enter movie name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should enter movie description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You should enter movie price")]
        public double Price { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        [Required(ErrorMessage = "You should enter movie trailer url")]
        public string TrailerUrl { get; set; }
        public MovieStatus Status { get; set; }
        [Required(ErrorMessage = "You should enter movie start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "You should enter movie end date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "You should choose one category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "You should choose one cinema")]
        public int CinemaId { get; set; }
        [ValidateNever]
        public Category category { get; set; }
        [ValidateNever]
        public Cinema cinema { get; set; }
        public int Counter { get; set; }
        [ValidateNever]
        public ICollection<ActorMovie> ActorMovies { get; } = new List<ActorMovie>();
        public ICollection<Cart> Carts { get; }=new List<Cart>();
    }
}
