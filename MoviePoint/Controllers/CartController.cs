using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviePoint.Models;
using MoviePoint.Models.ViewModels;
using MoviePoint.Repos.IRepos;
using Stripe;
using Stripe.Checkout;

namespace MoviePoint.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepo cartRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartRepo cartRepo, UserManager<ApplicationUser> userManager)
        {
            this.cartRepo = cartRepo;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = userManager.GetUserId(User);
            var carts = cartRepo.Get(Includation: [e => e.Movie, e => e.User], filter: e => e.ApplicationUserId == user);
            CartVM cartVM = new CartVM()
            {
                Carts = carts.ToList(),
                TotalSum = (double)carts.Sum(e => e.Movie.Price * e.Count)
            };
            return View(model: cartVM);
        }
        public IActionResult Increment(string userId, int Movieid)
        {
            var cart = cartRepo.GetOne(filter: e => e.MovieId == Movieid && e.ApplicationUserId == userId);
            cart.Count += 1;
            cartRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Decrement(string userId, int Movieid)
        {
            var cart = cartRepo.GetOne(filter: e => e.MovieId == Movieid && e.ApplicationUserId == userId);
            cart.Count -= 1;
            cartRepo.Attemp();
            if (cart.Count <= 0)
            {
                cartRepo.Delete(cart);
                cartRepo.Attemp();
                string CookieKey = $"Bsuccess_{userId}";
                var cookien = Request.Cookies[CookieKey];
                int count;
                count = int.Parse(cookien);
                count -= 1;
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddYears(1000);
                Response.Cookies.Append(CookieKey, count.ToString(), cookieOptions);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear(string userId, int Movieid)
        {
            var cart = cartRepo.GetOne(filter: e => e.MovieId == Movieid && e.ApplicationUserId == userId);
            cartRepo.Delete(cart);
            cartRepo.Attemp();
            string CookieKey = $"Bsuccess_{userId}";
            var cookien = Request.Cookies[CookieKey];
            int count;
            count = int.Parse(cookien);
            count -= 1;
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddYears(1000);
            Response.Cookies.Append(CookieKey, count.ToString(), cookieOptions);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Pay()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                CustomerEmail = userManager.GetUserAsync(User).Result.Email,
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Checkout/Success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Checkout/Cancel",
            };
            var carts = cartRepo.Get(Includation: [e => e.Movie, e => e.User], filter: e => e.ApplicationUserId == userManager.GetUserId(User)).ToList();
            foreach(var item in carts)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "Egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Movie.Name,
                            Description = item.Movie.Description
                        },
                        UnitAmount = (long)item.Movie.Price * 100
                    },
                    Quantity = item.Count
                });
            }
            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }
    }
}
