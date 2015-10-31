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
    public class AddressController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: /Address/
        public ActionResult Index()
        {
            if (!(Session["IsManagerLogged"] != null && Session["IsManagerLogged"].ToString().Equals(true.ToString())))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.Addresses.ToList());
        }

        // GET: /Address/Details/5
        public ActionResult Details(int? id)
        {
            if (!(Session["IsManagerLogged"] != null && Session["IsManagerLogged"].ToString().Equals(true.ToString())))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: /Address/Create
        public ActionResult Create()
        {
            if (!(Session["IsManagerLogged"] != null && Session["IsManagerLogged"].ToString().Equals(true.ToString())))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,AddressStore")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(address);
        }

        // GET: /Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!(Session["IsManagerLogged"] != null && Session["IsManagerLogged"].ToString().Equals(true.ToString())))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Adress")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: /Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!(Session["IsManagerLogged"] != null && Session["IsManagerLogged"].ToString().Equals(true.ToString())))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetAddresses()
        {
            return (Json(db.Addresses.ToList<Address>(),JsonRequestBehavior.AllowGet));   
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
