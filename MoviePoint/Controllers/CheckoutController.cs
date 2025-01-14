using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviePoint.Models;
using MoviePoint.Repos.IRepos;
using MoviePoint.Utility;

namespace MoviePoint.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartRepo cartRepo;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public CheckoutController(ICartRepo cartRepo, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            this.cartRepo = cartRepo;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }


        public IActionResult Success()
        {
            string emailBody = $@"
          <!DOCTYPE html>
<html lang='en'>

<head>
  <meta charset='UTF-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1.0' />
</head>

 <body style='height:100%;width:100%;'>

  <div style='box-shadow: 0px 0px 3px 0px;width:fit-content;background-color:white;padding: 1rem;border-radius: 5px;text-align: center;box-shadow: 0px 0px 7px 0px;margin-left:50%;transform:translateX(-50%);'>

    <img src='{Request.Scheme}://{Request.Host}/images/success.png' style='max-width: 8rem;'/>

    <p style='padding: 0.4rem 0.5rem;font-size: 1.1rem;font-weight: 500;'>Payment has been successfully done</p>
  </div>
</body>
</html>";
            var carts = cartRepo.Get(filter: e => e.ApplicationUserId == userManager.GetUserId(User));
            cartRepo.RemoveAll(carts.ToList());
            cartRepo.Attemp();
            var CookieKey = $"Bsuccess_{userManager.GetUserId(User)}";
            Response.Cookies.Delete(CookieKey);
            string user = userManager.GetUserAsync(User).Result.Email;
            emailSender.SendEmailAsync(user, "success", emailBody);
            return View();
        }
        public IActionResult Cancel()
        {
            #region emailbody
            string emailBody = $@"
          <!DOCTYPE html>
<html lang='en'>

<head>
  <meta charset='UTF-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1.0' />
</head>

 <body style='height:100%;width:100%;'>

  <div style='box-shadow: 0px 0px 3px 0px;width:fit-content;background-color:white;padding: 1rem;border-radius: 5px;text-align: center;box-shadow: 0px 0px 7px 0px;margin-left:50%;transform:translateX(-50%);'>

    <img src='{Request.Scheme}://{Request.Host}/images/wrong.png' style='max-width: 8rem;'/>

    <p style='padding: 0.4rem 0.5rem;font-size: 1.1rem;font-weight: 500;'>OOPS! There is an error during proccessing</p>
</body>
</html>"; 
            #endregion
            string user = userManager.GetUserAsync(User).Result.Email;
            emailSender.SendEmailAsync(user, "fail", emailBody);
            return View();
        }
    }
}
