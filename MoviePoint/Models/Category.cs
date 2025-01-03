using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You should enter a category name")]
        public string Name { get; set; }
        [ValidateNever]
        public ICollection<Movie> Movies { get; } = new List<Movie>();
    }
}
