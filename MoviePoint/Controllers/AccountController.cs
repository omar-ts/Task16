using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;

namespace MoviePoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Register()
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new("Admin"));
                await roleManager.CreateAsync(new("Customer"));
                await roleManager.CreateAsync(new("Cinema"));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserVM userVM, IFormFile file)
        {
            var EmailExixt = await userManager.FindByEmailAsync(userVM.Email);
            var UserExist = await userManager.FindByNameAsync(userVM.UserName);
            if (EmailExixt != null)
            {
                ModelState.AddModelError("Email", "This email is already exists");
            }
            if (UserExist != null)
            {
                ModelState.AddModelError("UserName", "This user name is already exists");
            }
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    userVM.ProfilePicture = fileName;
                }

                ApplicationUser user = new()
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    ProfilePicture = userVM.ProfilePicture
                };
                var result = await userManager.CreateAsync(user, userVM.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(userVM);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var EmailExist = await userManager.FindByEmailAsync(loginVM.Account);
                var UserExist = await userManager.FindByNameAsync(loginVM.Account);
                if (EmailExist != null || UserExist != null)
                {
                    var result = await userManager.CheckPasswordAsync(EmailExist ?? UserExist, loginVM.Password);
                    if (result)
                    {
                        await signInManager.SignInAsync(EmailExist ?? UserExist, loginVM.RememberMe);
                        return RedirectToAction("Index", "HomeMovie");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }
            return View(loginVM);
        }
        
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
