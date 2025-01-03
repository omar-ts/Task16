using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviePoint.Models
{
    public class CinemaReq
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CinemaLogo { get; set; }
        public string Address { get; set; }
        public string Detail { get; set; }
        public CStatus? Status { get; set; }
    }
}
