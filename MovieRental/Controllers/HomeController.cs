using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class HomeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        // GET: Home
        public ActionResult Index()
        {
            //ViewBag.Genres = db.Genres.ToList();
            return View(db.Genres.ToList());
            //return View();
        }

        
        public ActionResult GenreMenu()
        {
            var genres = db.Genres.ToList();

            return PartialView(genres);
        }
    }
}