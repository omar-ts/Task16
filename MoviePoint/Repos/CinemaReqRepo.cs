using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Repos
{
    public class CinemaReqRepo : Repo<CinemaReq>, ICinemaReqRepo
    {
        public CinemaReqRepo(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
