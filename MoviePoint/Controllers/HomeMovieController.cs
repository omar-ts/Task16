using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class HomeMovieController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        IMovieRepo MovieRepo;
        private readonly IActorRepo actorRepo;
        private readonly ICartRepo cartRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeMovieController(IMovieRepo movieRepo, IActorRepo actorRepo, ICartRepo cartRepo, UserManager<ApplicationUser> userManager)
        {
            MovieRepo = movieRepo;
            this.actorRepo = actorRepo;
            this.cartRepo = cartRepo;
            this.userManager = userManager;
        }

        public IActionResult Index(int pagination = 1, string? MovieName = null)
        {
            var movies = MovieRepo.Get(Includation: [e => e.category, e => e.cinema]);
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
            if (MovieName != null)
            {
                MovieName = MovieName.Trim();
                movies = MovieRepo.Get(Includation: [e => e.category, e => e.cinema], filter: e => e.Name.Contains(MovieName));
            }
            if (!movies.Any())
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            double totalPages = Math.Ceiling((double)(movies.Count()) / 4);
            movies = movies.Skip((pagination - 1) * 4).Take(4);
            int startPage = Math.Max(1, pagination - 1);
            int endPage = Math.Min((int)totalPages, pagination + 1);
            MovieVM movieVM = new()
            {
                Movies = movies.ToList(),
                TotalPages = totalPages,
                Pagination = pagination,
                StartPage = startPage,
                EndPage = endPage
            };
            return View(movieVM);

        }
        public IActionResult Book(int id)
        {
            var movie = MovieRepo.GetOne(Includation: [e => e.category, e => e.cinema, e => e.ActorMovies], filter: e => e.Id == id);
            actorRepo.Get(Includation: [e => e.ActorMovies]).ToList();
            if (DateTime.Now > movie.EndDate)
            {
                movie.Status = MovieStatus.Expired;
            }
            else if (DateTime.Now < movie.StartDate)
            {
                movie.Status = MovieStatus.Upcoming;
            }
            else if (DateTime.Now >= movie.StartDate && DateTime.Now <= movie.EndDate)
            {
                movie.Status = MovieStatus.Available;
            }
            return View(model: movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(int movieId, int counter)
        {
            var user = userManager.GetUserId(User);
            var cartExist = cartRepo.GetOne(filter: e => e.ApplicationUserId == user && e.MovieId == movieId);
            if (cartExist != null)
            {
                cartExist.Count = cartExist.Count + counter;
                cartRepo.Attemp();
                TempData["Success"] = "Your cart has been successfully changed";
            }
            else if (cartExist == null)
            {
                Cart cart = new()
                {
                    ApplicationUserId = user,
                    MovieId = movieId,
                    Count = counter,
                    Time = DateTime.Now
                };
                cartRepo.Create(cart);
                cartRepo.Attemp();
                TempData["Success"] = "You have been successfully booked";
                string CookieKey = $"Bsuccess_{user}";
                var cookien = Request.Cookies[CookieKey];
                if (cookien == null) cookien = "0";
                int count = int.Parse(cookien);
                count += 1;
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddYears(1000);
                Response.Cookies.Append(CookieKey, count.ToString(), cookieOptions);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult NotFoundPage()
        {
            return View();
        }
        public IActionResult Clear()
        {
            var user = userManager.GetUserId(User);
            Response.Cookies.Delete($"Bsuccess_{user}");
            return RedirectToAction(nameof(Index));
        }
    }
}
