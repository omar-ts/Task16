using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class CinemaDetail : Controller
    {
        ICinemaRepo cinemaRepo;
        public CinemaDetail(ICinemaRepo cinemaRepo)
        {
            this.cinemaRepo = cinemaRepo;
        }

        public IActionResult Index(int id)
        {
            var cinema = cinemaRepo.GetWith(Includation: [e => e.Movies], filtone: e => e.Id == id);
            var movies = cinemaRepo.GetMovCat(Includen: [e => e.category]).ToList();
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
            return View(cinema);
        }
    }
}
