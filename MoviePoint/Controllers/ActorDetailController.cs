using Microsoft.AspNetCore.Mvc;
using MoviePoint.Data;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class ActorDetailController : Controller
    {
        IActorRepo actorRepo;
        public ActorDetailController(IActorRepo actorRepo)
        {
            this.actorRepo = actorRepo;
        }

        public IActionResult Index(int id)
        {
            var actor = actorRepo.GetOne(filter:e=>e.Id==id);
            return View(model: actor);
        }
    }
}
