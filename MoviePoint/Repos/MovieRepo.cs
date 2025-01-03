using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;
using System.Linq.Expressions;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class MovieRepo : Repo<Movie>, IMovieRepo
    {
        public MovieRepo(ApplicationDbContext dbContext) : base(dbContext) { }

        public Movie GetWith(Expression<Func<Movie, bool>>? filter = null, Expression<Func<Movie, object>>[]? Includation = null, Expression<Func<Movie, bool>>? filtone = null, bool tracked = true)
        {
            return Get(filter, Includation, tracked).FirstOrDefault(filtone);
        }

        public IQueryable<Actor> GetActorMov(Expression<Func<Actor, object>>[]? Includen = null)
        {
            IQueryable<Actor> query = _dbContext.Actors;
            if (Includen != null)
            {
                foreach (var item in Includen)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
