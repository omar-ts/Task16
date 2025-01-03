using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class MovieDetailController : Controller
    {
        IMovieRepo movieRepo;
        public MovieDetailController(IMovieRepo movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        public IActionResult Index(int id)
        {
            var movies = movieRepo.GetWith(Includation: [e => e.category, e => e.cinema, e => e.ActorMovies], filtone: e => e.Id == id);
            movieRepo.GetActorMov(Includen: [e => e.ActorMovies]).ToList();
            if (DateTime.Now > movies.EndDate)
            {
                movies.Status = MovieStatus.Expired;
            }
            else if (DateTime.Now < movies.StartDate)
            {
                movies.Status = MovieStatus.Upcoming;
            }
            else if (DateTime.Now >= movies.StartDate && DateTime.Now <= movies.EndDate)
            {
                movies.Status = MovieStatus.Available;
            }
            movies.Counter = movies.Counter + 1;
            movieRepo.Attemp();
            return View(model: movies);
        }
    }
}
