using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos;
using MoviePoint.Repos.IRepos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MoviePoint.Controllers
{
    public class AllActorController : Controller
    {
        IActorRepo actorRepo;
        public AllActorController(IActorRepo actorRepo)
        {
            this.actorRepo = actorRepo;
        }

        public IActionResult Index(int pagination=1,string? query=null)
        {
            var actors = actorRepo.Get();
            if (query != null)
            {
                query = query.Trim();
                actors = actorRepo.Get(filter: e => e.Name.Contains(query));
            }
            double totalPages = Math.Ceiling((double)(actors.Count()) / 3);
            actors = actors.Skip((pagination - 1) * 3).Take(3);
            int startPage = Math.Max(1, pagination - 1);
            int endPage = Math.Min((int)totalPages, pagination + 1);
            ActorVM actorVM = new()
            {
                Actors = actors.ToList(),
                TotalPages = totalPages,
                Pagination = pagination,
                StartPage = startPage,
                EndPage = endPage
            };
            return View(actorVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Actor actor, IFormFile file)
        {
            ModelState.Remove("file");
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    actor.ProfilePicture = fileName;
                }
                actorRepo.Create(actor);
                actorRepo.Attemp();
                TempData["ASuccess"] = "Actor is successfully created";
                return RedirectToAction(nameof(Index));
            }
            return View(model: actor);
        }
        public IActionResult Edit(int id)
        {
            var Actorr = actorRepo.GetOne(filter:e=>e.Id==id);
            return View(model: Actorr);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Actor actor, IFormFile file)
        {
            ModelState.Remove("file");
            var OldProduct = actorRepo.GetOne(filter: e => e.Id == actor.Id, tracked: false);
            var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", OldProduct.ProfilePicture);
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    if (System.IO.File.Exists(OldPath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                    actor.ProfilePicture = fileName;
                }
                else
                {
                    actor.ProfilePicture = OldProduct.ProfilePicture;
                }

                actorRepo.Edit(actor);
                actorRepo.Attemp();
                TempData["ASuccess"] = "Actor is successfully edited";
                return RedirectToAction(nameof(Index));
            }
            return View(model: actor);
        }
        public IActionResult Delete(int id)
        {
            var actor = actorRepo.GetOne(filter: e => e.Id == id);
            var OldProduct = actorRepo.GetOne(filter: e => e.Id == actor.Id, tracked: false);
            var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", OldProduct.ProfilePicture);
            if (System.IO.File.Exists(OldPath))
            {
                System.IO.File.Delete(OldPath);
            }
            actorRepo.Delete(actor);
            actorRepo.Attemp();
            TempData["ASuccess"] = "Actor is successfully deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
