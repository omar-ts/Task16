using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class HomeCategoryController : Controller
    {
        ICategoryRepo CategoryRepo;
        public HomeCategoryController(ICategoryRepo categoryRepo)
        {
            CategoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var categories = CategoryRepo.Get().ToList();
            return View(model: categories);
        }
    }
}
