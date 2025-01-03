using Microsoft.EntityFrameworkCore;
using MoviePoint.Models;
using System.Linq.Expressions;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos.IRepos
{
    public interface ICategoryRepo:IRepo<Category>
    {
        public Category GetWith(Expression<Func<Category, bool>>? filter = null, Expression<Func<Category, object>>[]? Includation = null, Expression<Func<Category, bool>>? filtone = null, bool tracked = true);
        public IQueryable<Movie> GetMovCin(Expression<Func<Movie, object>>[]? Includen = null);
    }
}
