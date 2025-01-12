using Microsoft.EntityFrameworkCore;
using MoviePoint.Models;
using System.Linq.Expressions;
using TPoint.Repos.IRepos;

namespace MoviePoint.Repos.IRepos
{
    public interface ICinemaRepo : IRepo<Cinema>
    {
    }
}
