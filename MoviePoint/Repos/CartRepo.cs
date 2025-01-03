using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class CartRepo:Repo<Cart>,ICartRepo
    {
        public CartRepo(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
