using Microsoft.EntityFrameworkCore;
using MoviePoint.Models;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos.IRepos
{
    public interface IActorMovieRepo:IRepo<ActorMovie>
    {
        public IQueryable<T> GetAll<T>() where T : class;
    }
}
