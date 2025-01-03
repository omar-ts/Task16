using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class ActorMovieRepo:Repo<ActorMovie>,IActorMovieRepo
    {
        public ActorMovieRepo(ApplicationDbContext dbContext) : base(dbContext) { }

        public IQueryable<T>GetAll<T>()where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
