using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace TPoint.Repos.IRepos
{
    public interface IRepo<T>where T : class
    {
        public void Create(T entity);
        public void Edit(T entity);
        public void Delete(T entity);
        public void Attemp();
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? Includation = null, bool tracked = true);
        public T GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? Includation = null, bool tracked = true);
    }
}
