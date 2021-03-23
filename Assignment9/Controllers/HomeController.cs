using Assignment9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment9.Controllers
{
    public class HomeController : Controller
    {

        private MovieListContext context { get; set; }
        // Constructor
        public HomeController(MovieListContext con)
        {
            context = con;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        // Route to view all movies
        [HttpGet]
        public IActionResult ViewMovie()
        {
            return View(context.Movies);
        }

        // Delete a movie from the database
        [HttpPost]
        public IActionResult Delete(int movieId)
        {
            EnterMovie m = context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                context.Movies.Remove(m);
                context.SaveChanges();
            }
            return View("Deleted");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Add a route EnterMovie movie view fresh form
        [HttpGet]
        public IActionResult EnterMovie()
        {
            return View();
        }

        // Add a route to the EnterMovie Confirmation View after submit button
        [HttpPost]
        public IActionResult EnterMovie(EnterMovie m)
        {
            if (ModelState.IsValid)
            {
                context.Movies.Add(m);
                context.SaveChanges();
                return View("Confirmation");
            }
            else
            {
                return View("Error");
            }
     
        }
        
        // Update database
        public IActionResult Update(int movieId)
        {
            EnterMovie m = context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
            return View(m);
        }

        [HttpPost]
        public IActionResult Update(EnterMovie m, int movieId)
        {
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Category = m.Category;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Title = m.Title;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Year = m.Year;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Director = m.Director;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Rating = m.Rating;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Edited = m.Edited;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().LentTo = m.LentTo;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Notes = m.Notes;
            context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Deleted()
        {
            return View();
        }
        public IActionResult Confirmation()
        {
            TempData["ResultMessage"] = "YOU DID IT WOOT WOOT";
            return View();
        }

        // Add a route to the MyPodcasts View
        public IActionResult MyPodcast()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
