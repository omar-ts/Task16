using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class ApplicationUserRepo:Repo<ApplicationUser>,IApplicationUserRepo
    {
        public ApplicationUserRepo(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
