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
            return View();
        }

        
        public ActionResult GenreMenu()
        {
            var genres = db.Genres.ToList();

            return PartialView(genres);
        }

        public ActionResult Report()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AboutUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "Username, Password")] User user)
        {
            if (user.Username != null && user.Password != null)
            {
                var v = db.Users.Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password)).FirstOrDefault();
                if (v != null)
                {
                    ViewBag.Message = null;

                    Session["LoggedUserId"] = v.ID.ToString();
                    Session["LoggedUserName"] = v.Username;
                    Session["IsManagerLogged"] = v.IsManager.ToString();
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "Wrong Username or Password";
            }
            return View(user);
        }

        public ActionResult LogOut()
        {
            Session["LoggedUserId"] = null;
            Session["LoggedUserName"] = null;
            Session["IsManagerLogged"] = null;
            return RedirectToAction("Index");
        }
    }
}