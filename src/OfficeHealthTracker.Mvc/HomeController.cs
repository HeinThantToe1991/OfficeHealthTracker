﻿using Microsoft.AspNetCore.Mvc;

namespace OfficeHealthTracker.Mvc
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
