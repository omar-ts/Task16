using Microsoft.EntityFrameworkCore;
using MoviePoint.Models;
using System.Data;
using System.Linq.Expressions;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos.IRepos
{
    public interface IMovieRepo:IRepo<Movie>
    {
        public Movie GetWith(Expression<Func<Movie, bool>>? filter = null, Expression<Func<Movie, object>>[]? Includation = null, Expression<Func<Movie, bool>>? filtone = null, bool tracked = true);
        public IQueryable<Actor> GetActorMov(Expression<Func<Actor, object>>[]? Includen = null);
        public IQueryable<T> GetAll<T>() where T : class;
    }
}
