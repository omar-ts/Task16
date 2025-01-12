using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class CinemaDetail : Controller
    {
        ICinemaRepo cinemaRepo;
        private readonly IMovieRepo movieRepo;

        public CinemaDetail(ICinemaRepo cinemaRepo, IMovieRepo movieRepo)
        {
            this.cinemaRepo = cinemaRepo;
            this.movieRepo = movieRepo;
        }

        public IActionResult Index(int id,int pagination = 1)
        {
            var cinema = cinemaRepo.GetOne(Includation: [e => e.Movies], filter: e => e.Id == id);
            var movies = movieRepo.Get(Includation: [e => e.category]);
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
            double totalPages = Math.Ceiling((double)(movies.Count()) / 4);
            movies = movies.Skip((pagination - 1) * 4).Take(4);
            int startPage = Math.Max(1, pagination - 1);
            int endPage = Math.Min((int)totalPages, pagination + 1);
            CinemanVM cinemanVM = new()
            {
                Cinema = cinema,
                Movies=movies.ToList(),
                Pagination=pagination,
                StartPage=startPage,
                EndPage=endPage,
                TotalPages=totalPages
            };
            return View(cinemanVM);
        }
    }
}
