using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos;
using MoviePoint.Repos.IRepos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MoviePoint.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IApplicationUserRepo applicationUserRepo;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IApplicationUserRepo applicationUserRepo)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationUserRepo = applicationUserRepo;
        }
        public IActionResult Index(int pagination = 1, string? query = null)
        {
            var users = applicationUserRepo.Get();
            if (query != null)
            {
                query = query.Trim();
                users = applicationUserRepo.Get(filter: e => e.UserName.Contains(query));
            }
            double totalPages = Math.Ceiling((double)(users.Count()) / 3);
            users = users.Skip((pagination - 1) * 3).Take(3);
            int startPage = Math.Max(1, pagination - 1);
            int endPage = Math.Min((int)totalPages, pagination + 1);
            UserIndVM userIndVM = new()
            {
                users = users.ToList(),
                TotalPages = totalPages,
                Pagination = pagination,
                StartPage = startPage,
                EndPage = endPage
            };
            return View(model: userIndVM);
        }
        public IActionResult Block(string id)
        {
            var user = applicationUserRepo.GetOne(filter: e => e.Id == id);
            user.LockoutEnd = DateTime.Now.AddYears(1000);
            applicationUserRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult UnBlock(string id)
        {
            var user = applicationUserRepo.GetOne(filter: e => e.Id == id);
            user.LockoutEnd = null;
            applicationUserRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Manage(string id)
        {
            var user = applicationUserRepo.GetOne(filter: e => e.Id == id);
            var role = roleManager.Roles.ToList();
            UserRoleVM userRoleVM = new UserRoleVM()
            {
                AppUser = user,
                Role = role
            };
            return View(model: userRoleVM);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(string userId, string RoleName)
        {
            var user=applicationUserRepo.GetOne(filter:e => e.Id == userId);
            var roles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user,roles);
            var role = await roleManager.FindByIdAsync(RoleName);
            await userManager.AddToRoleAsync(user,role.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}
