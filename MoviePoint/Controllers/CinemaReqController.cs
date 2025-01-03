using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class CinemaReqController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICinemaReqRepo cinemaReqRepo;
        private readonly ICinemaRepo cinemaRepo;

        public CinemaReqController(UserManager<ApplicationUser> userManager, ICinemaReqRepo cinemaReqRepo, ICinemaRepo cinemaRepo)
        {
            this.userManager = userManager;
            this.cinemaReqRepo = cinemaReqRepo;
            this.cinemaRepo = cinemaRepo;
        }
        public IActionResult Index(int pagination = 1, string? query = null)
        {
            var cinemaReq = cinemaReqRepo.Get();
            if (query != null)
            {
                query = query.Trim();
                cinemaReq = cinemaReqRepo.Get(filter: e => e.Name.Contains(query));
            }
            double totalPages = Math.Ceiling((double)(cinemaReq.Count()) / 3);
            cinemaReq = cinemaReq.Skip((pagination - 1) * 3).Take(3);
            int startPage = Math.Max(1, pagination - 1);
            int endPage = Math.Min((int)totalPages, pagination + 1);
            CinemaReqVM cinemaReqVM = new()
            {
                CinemaReqs = cinemaReq.ToList(),
                TotalPages = totalPages,
                Pagination = pagination,
                StartPage = startPage,
                EndPage = endPage
            };
            return View(cinemaReqVM);
        }
        public IActionResult CookDel()
        {
            Response.Cookies.Append("Success", "");
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Requeste(string Email)
        {
            var UserEmail = await userManager.FindByEmailAsync(Email);
            var cinemafind = cinemaReqRepo.GetOne(filter: e => e.Email == Email);
            var cinema = cinemaRepo.GetOne(filter: e => e.Name == UserEmail.FirstName);
            if (cinemafind == null && cinema==null)
            {
                CinemaReq cinemaReq = new()
                {
                    Email = UserEmail.Email,
                    Name = UserEmail.FirstName,
                    CinemaLogo = UserEmail.ProfilePicture,
                    Address = UserEmail.Address,
                    Detail = UserEmail.Description
                };
                cinemaReqRepo.Create(cinemaReq);
                cinemaReqRepo.Attemp();
                TempData["Success"] = "Your request have been sent successfully";
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddYears(1000);
                Response.Cookies.Append("Success", "add request successfully", cookieOptions);
                return RedirectToAction("Index", "HomeMovie");
            }
            TempData["Fail"] = "You have been requested before";
            return RedirectToAction("Index", "HomeMovie");
        }
        public IActionResult Accepted(int id)
        {
            var cinemaReq = cinemaReqRepo.GetOne(filter: e => e.Id == id);
            Cinema cinema = new()
            {
                Name = cinemaReq.Name,
                Description = cinemaReq.Detail,
                CinemaLogo = cinemaReq.CinemaLogo,
                Address = cinemaReq.Address,
            };
            cinemaRepo.Create(cinema);
            cinemaRepo.Attemp();
            cinemaReqRepo.Delete(cinemaReq);
            cinemaReqRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Rejected(int id)
        {
            var cinemaReq = cinemaReqRepo.GetOne(filter: e => e.Id == id);
            cinemaReq.Status = CStatus.Rejected;
            cinemaReqRepo.Edit(cinemaReq);
            cinemaReqRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
    }
}
