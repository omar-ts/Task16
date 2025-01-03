using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class HomeCinemaController : Controller
    {
        ICinemaRepo CinemaRepo;
        public HomeCinemaController(ICinemaRepo CinemaRepo)
        {
            this.CinemaRepo = CinemaRepo;
        }
        public IActionResult Index()
        {
            var cinemas = CinemaRepo.Get().ToList();
            return View(model: cinemas);
        }
    }
}
