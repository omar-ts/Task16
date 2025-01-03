using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos.IRepos;
using System.Text.RegularExpressions;

namespace MoviePoint.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            return View(model: user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ApplicationUser applicationUser, IFormFile file)
        {
            var OldFiles = await userManager.GetUserAsync(User);
            var user = await userManager.GetUserAsync(User);
            var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", OldFiles.ProfilePicture);
            if (applicationUser.PhoneNumber != null)
            {
                if (applicationUser.PhoneNumber.Length != 11 || !Regex.IsMatch(applicationUser.PhoneNumber, @"^\d+$"))
                {
                    ModelState.AddModelError("PhoneNumber", "Number must be 11 digits");
                }
            }
            ModelState.Remove("file");
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
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                    applicationUser.ProfilePicture = fileName;
                }
                else
                {
                    applicationUser.ProfilePicture = OldFiles.ProfilePicture;
                }
                user.ProfilePicture = applicationUser.ProfilePicture;
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.PhoneNumber = applicationUser.PhoneNumber;
                var updates = await userManager.UpdateAsync(user);
                if (updates.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    TempData["Isuccess"] = "Profile is successfully changed";
                }
            }
            else if (!ModelState.IsValid)
            {
                applicationUser.ProfilePicture = OldFiles.ProfilePicture;
            }
            return View(applicationUser);
        }
        public async Task<IActionResult> ChEmail()
        {
            var user = await userManager.GetUserAsync(User);
            return View(model: user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChEmail(ApplicationUser applicationUser, string NewEmail)
        {
            var user = await userManager.GetUserAsync(User);
            if (NewEmail != null)
            {
                var EmailExist = await userManager.FindByEmailAsync(NewEmail);
                if (EmailExist != null)
                {
                    ModelState.AddModelError("Email", "try another email");
                }
                else
                {
                    applicationUser.Email = NewEmail;
                    user.Email = applicationUser.Email;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        TempData["Esuccess"] = "Email is successfully changed";
                    }
                }
            }
            return View(applicationUser);
        }
        public IActionResult ChPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChPassword(PassUserVM passUserVM)
        {
            if (ModelState.IsValid)
            {
                var UserExists = await userManager.FindByNameAsync(passUserVM.UserName);
                if (UserExists != null)
                {
                    var result = await userManager.ChangePasswordAsync(UserExists, passUserVM.CurrentPassword, passUserVM.NewPassword);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(UserExists, false);
                        TempData["Psuccess"] = "Password is successfully changed";
                    }
                    else
                    {
                        ModelState.AddModelError("CurrentPassword", "Password is not correct");
                    }
                }
            }
            return View();
        }
    }
}
