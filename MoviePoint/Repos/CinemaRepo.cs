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
    }
}
