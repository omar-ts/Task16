using Microsoft.EntityFrameworkCore;
using MoviePoint.Models;
using System.Linq.Expressions;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos.IRepos
{
    public interface ICinemaRepo:IRepo<Cinema>
    {
        public Cinema GetWith(Expression<Func<Cinema, bool>>? filter = null, Expression<Func<Cinema, object>>[]? Includation = null, Expression<Func<Cinema, bool>>? filtone = null, bool tracked = true);

        public IQueryable<Movie> GetMovCat(Expression<Func<Movie, object>>[]? Includen = null);
    }
}
