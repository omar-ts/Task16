using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class CategoryDetailController : Controller
    {
        ICategoryRepo categoryRepo;
        public CategoryDetailController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public IActionResult Index(int id)
        {
            var category = categoryRepo.GetWith(Includation: [e => e.Movies], filtone: e => e.Id == id);
            var movies = categoryRepo.GetMovCin(Includen: [e => e.cinema]).ToList();
            foreach (var item in movies)
            {
                if (DateTime.Now > item.EndDate)
                {
                    item.Status = MovieStatus.Expired;
                }
                else if (DateTime.Now < item.StartDate)
                {
                    item.Status = MovieStatus.Upcoming;
                }
                else if (DateTime.Now >= item.StartDate && DateTime.Now <= item.EndDate)
                {
                    item.Status = MovieStatus.Available;
                }
            }
            return View(model: category);
        }
    }
}
