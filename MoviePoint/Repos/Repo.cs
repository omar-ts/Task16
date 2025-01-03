using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using System.Linq.Expressions;
using System.Linq;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class Repo<T> : IRepo<T> where T : class
    {
        protected ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;
        public Repo(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbSet=_dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Edit(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Attemp()
        {
            _dbContext.SaveChanges();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? Includation = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (Includation != null)
            {
                foreach (var item in Includation)
                {
                    query = query.Include(item);
                }
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public T GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? Includation = null, bool tracked = true)
        {
            return Get(filter, Includation, tracked).FirstOrDefault();
        }
    }
}
