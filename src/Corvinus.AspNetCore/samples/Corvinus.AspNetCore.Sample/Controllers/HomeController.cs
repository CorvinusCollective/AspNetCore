// <copyright file="HomeController.cs" company="Corvinus Collective">
// Copyright (c) Corvinus Collective. All rights reserved.
// </copyright>

namespace Corvinus.AspNetCore.Sample.Controllers
{
    using System.Diagnostics;
    using Corvinus.AspNetCore.Sample.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index View.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the Index View.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Privacy View.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the Privacy View.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Index View.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the Error View.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
