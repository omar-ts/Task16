using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;
using System.Linq.Expressions;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class CinemaRepo:Repo<Cinema>,ICinemaRepo
    {
        public CinemaRepo(ApplicationDbContext dbContext) : base(dbContext) { }

        public Cinema GetWith(Expression<Func<Cinema, bool>>? filter = null, Expression<Func<Cinema, object>>[]? Includation = null, Expression<Func<Cinema, bool>>? filtone = null, bool tracked = true)
        {
            return Get(filter, Includation, tracked).FirstOrDefault(filtone);
        }

        public IQueryable<Movie> GetMovCat(Expression<Func<Movie, object>>[]? Includen = null)
        {
            IQueryable<Movie> query = _dbContext.Movies;
            if (Includen != null)
            {
                foreach (var item in Includen)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
