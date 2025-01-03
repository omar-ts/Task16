using Microsoft.AspNetCore.Mvc;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class AllCategoryController : Controller
    {
        ICategoryRepo categoryRepo;

        public AllCategoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public IActionResult Index(int pagination = 1, string? query = null)
        {
            var categories = categoryRepo.Get();
            if (query != null)
            {
                query = query.Trim();
                categories = categoryRepo.Get(filter: e => e.Name.Contains(query));
            }
            double totalPages = Math.Ceiling((double)(categories.Count()) / 3);
            categories = categories.Skip((pagination - 1) * 3).Take(3);
            int startPage = Math.Max(1, pagination - 1);
            int endPage = Math.Min((int)totalPages, pagination + 1);
            CategoryVM categoryVM = new()
            {
                Categories = categories.ToList(),
                TotalPages = totalPages,
                Pagination = pagination,
                StartPage = startPage,
                EndPage = endPage
            };
            return View(categoryVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepo.Create(category);
                categoryRepo.Attemp();
                TempData["Success"] = "Category is successfully created";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Edit(int id)
        {
            var category = categoryRepo.GetOne(filter: e => e.Id == id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepo.Edit(category);
                categoryRepo.Attemp();
                TempData["Success"] = "Category is successfully edited";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            var category = categoryRepo.GetOne(filter: e => e.Id == id);
            categoryRepo.Delete(category);
            categoryRepo.Attemp();
            TempData["Success"] = "Category is successfully deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
