using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Reservation3.Models;

namespace Reservation3.Controllers
{
    public class ReservationsController : Controller
    {
        private ReservationDBEntities db = new ReservationDBEntities();
        [Authorize]
        // GET: Reservations
        public ActionResult Index()
        {
            return View(db.Reservation.ToList());
        }
        [Authorize]
        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }
        [Authorize]
        // GET: Reservations/Create
        public ActionResult Create()
        {
            List<SelectListItem> from = new List<SelectListItem>();
            from.Add(new SelectListItem() { Text = "Rabat", Value = "Rabat" });
            from.Add(new SelectListItem() { Text = "Fes", Value = "Fes" });
            from.Add(new SelectListItem() { Text = "Paris", Value = "Paris" });
            from.Add(new SelectListItem() { Text = "London", Value = "London" });
            from.Add(new SelectListItem() { Text = "Rome", Value = "Rome" });
            from.Add(new SelectListItem() { Text = "Istanboul", Value = "Istanboul" });
            from.Add(new SelectListItem() { Text = "New York", Value = "New York" });
            from.Add(new SelectListItem() { Text = "Madrid", Value = "Madrid" });
            from.Add(new SelectListItem() { Text = "Cairo", Value = "Cairo" });
            ViewBag.From = from;


            List<SelectListItem> to = new List<SelectListItem>();
            to.Add(new SelectListItem() { Text = "Fes", Value = "Fes" });
            to.Add(new SelectListItem() { Text = "Paris", Value = "Paris" });
            to.Add(new SelectListItem() { Text = "London", Value = "London" });
            to.Add(new SelectListItem() { Text = "Rome", Value = "Rome" });
            to.Add(new SelectListItem() { Text = "Istanboul", Value = "Istanboul" });
            to.Add(new SelectListItem() { Text = "New York", Value = "New York" });
            to.Add(new SelectListItem() { Text = "Madrid", Value = "Madrid" });
            to.Add(new SelectListItem() { Text = "Cairo", Value = "Cairo" });
            to.Add(new SelectListItem() { Text = "Rabat", Value = "Rabat" });
            ViewBag.To = to;
    
            
                 List<SelectListItem> nombre_de_place = new List<SelectListItem>();
             nombre_de_place.Add(new SelectListItem() { Text = "40", Value = "40" });
             nombre_de_place.Add(new SelectListItem() { Text = "45", Value = "45" });
             nombre_de_place.Add(new SelectListItem() { Text = "50", Value = "50" });
             nombre_de_place.Add(new SelectListItem() { Text = "60", Value = "60" });
             nombre_de_place.Add(new SelectListItem() { Text = "80", Value = "80" });
             nombre_de_place.Add(new SelectListItem() { Text = "100", Value = "100" });
 
                ViewBag.nombre_de_place = nombre_de_place;
            return View();
        }

        // POST: Reservations/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,From,To,date,time,prix,nombre_de_place,date_de_retour,time_de_retour,way")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }
        [Authorize]
        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,From,To,date,time,prix,nombre_de_place,date_de_retour,time_de_retour,way")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Chercher(string depart, string destination, string date, string time)
        {
            List<Reservation> a = new List<Reservation>();
            List<Reservation> L = new List<Reservation>();
            Reservation f = new Reservation();
            f.id = 0;
            f.From = "vide";
            a.Add(f);
            if (string.IsNullOrEmpty(depart)
                  && string.IsNullOrEmpty(destination)
                 )
            {
                foreach (Reservation elem in db.Reservation)
                {
                    elem.From = "vide";
                    L.Add(elem);
                }

            }

            else
            {
                foreach (Reservation elem in db.Reservation)
                {
                    if (string.Compare(depart, elem.From, StringComparison.OrdinalIgnoreCase) == 0
                        && string.Compare(destination, elem.To, StringComparison.OrdinalIgnoreCase) == 0
                          )
                    {

                        L.Add(elem);
                    }
                }
            }
            if (!L.Any())
            {
                L = a;
            }
            return View(L);
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
