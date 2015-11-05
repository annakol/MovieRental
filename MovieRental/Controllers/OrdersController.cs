using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRental.Models;

namespace MovieRental.Controllers
{
    public class OrdersController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Orders
        public ActionResult Index(string title,string username,bool? notReturned)
        {
            if (!(Session["IsManagerLogged"] != null && Session["IsManagerLogged"].ToString().Equals(true.ToString())))
            {
                return RedirectToAction("Details");
            }
            var orders = db.Orders.Include(o => o.Movie).Include(o => o.User);

            if (!String.IsNullOrEmpty(title))
            {
                orders = orders.Where(o => o.Movie.Title.Contains(title));
            }
            if (!String.IsNullOrEmpty(username))
            {
                orders = orders.Where(o => o.User.Username.Contains(username));
            }
            if (notReturned != null && notReturned.Value)
            {
                orders = orders.Where(o => o.ReturnDate == null);
            }

            return View(orders.ToList());
        }

        public ActionResult Details()
        {
            if (Session["LoggedUserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int UserId = Int32.Parse(Session["LoggedUserId"].ToString());
            var ordersQuery = 
                (from o in db.Orders
                 where o.UserId == UserId
                 join m in db.Movies on o.MovieId equals m.MovieId
                 select new { m.Title, m.Price, o.OrderDate, o.ReturnDate });

            var orders = new List<Order>();
            foreach(var t in ordersQuery)
            {
                orders.Add(new Order()
                {
                    Movie = new Movie() {
                        Title = t.Title,
                        Price = t.Price
                    },
                    OrderDate = t.OrderDate,
                    ReturnDate = t.ReturnDate
                });
            }

            return View(orders.ToList());
        }

        // GET: Orders/Create
        public ActionResult Create(int? id)
        {
            if (Session["LoggedUserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return PartialView(movie);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.UserId = Int32.Parse(Session["LoggedUserId"].ToString());
                order.OrderDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "Title", order.MovieId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName", order.UserId);
            return View(order);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["LoggedUserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return PartialView(order);
        }

        // POST: Movies/Delete/5
        public JsonResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            order.ReturnDate = DateTime.Now;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            var jsonOrder = new Order()
            {
                OrderId = order.OrderId,
                Movie = new Movie()
                {
                    Title = order.Movie.Title
                },
                User = new User() 
                {
                    Username = order.User.Username
                },
                OrderDate = order.OrderDate,
                ReturnDate = order.ReturnDate
            };
            return Json(jsonOrder, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
