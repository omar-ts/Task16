﻿using Microsoft.AspNetCore.Mvc;

namespace MoviePoint.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
