﻿using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using System.Diagnostics;

namespace NLayer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _services;

        public HomeController(IProductService services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _services.GetProductsWithCategory());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
