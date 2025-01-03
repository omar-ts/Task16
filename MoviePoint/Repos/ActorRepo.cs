using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class ActorRepo:Repo<Actor>,IActorRepo
    {
        public ActorRepo(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
