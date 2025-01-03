using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;
using System.Linq.Expressions;

namespace MoviePoint.Repos
{
    public class CategoryRepo:Repo<Category>,ICategoryRepo
    {
        public CategoryRepo(ApplicationDbContext dbContext) : base(dbContext) { }

        public Category GetWith(Expression<Func<Category, bool>>? filter = null, Expression<Func<Category, object>>[]? Includation = null, Expression<Func<Category, bool>>? filtone = null, bool tracked = true)
        {
            return Get(filter, Includation, tracked).FirstOrDefault(filtone);
        }

        public IQueryable<Movie> GetMovCin(Expression<Func<Movie, object>>[]? Includen = null)
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
