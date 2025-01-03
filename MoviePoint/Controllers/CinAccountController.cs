using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos;
using MoviePoint.Repos.IRepos;

namespace MoviePoint.Controllers
{
    public class CinAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public CinAccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CinRegistVM cinRegistVM, IFormFile file)
        {
            var EmailExist = await userManager.FindByEmailAsync(cinRegistVM.Email);
            var UserExist = await userManager.FindByNameAsync(cinRegistVM.UserName);
            if (EmailExist != null)
            {
                ModelState.AddModelError("Email", "this email is already exists");
            }
            if (UserExist != null)
            {
                ModelState.AddModelError("UserName", "this user name is already exists");
            }
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    cinRegistVM.CinemaLogo = fileName;
                }
                ApplicationUser user = new()
                {
                    UserName = cinRegistVM.UserName,
                    Email = cinRegistVM.Email,
                    FirstName = cinRegistVM.Name,
                    LastName = cinRegistVM.Name,
                    ProfilePicture = cinRegistVM.CinemaLogo,
                    Address = cinRegistVM.Address,
                    Description = cinRegistVM.Detail
                };
                var result = await userManager.CreateAsync(user, cinRegistVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Cinema");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(cinRegistVM);
        }
    }
}
