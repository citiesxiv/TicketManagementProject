using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketManagementProject;
using TicketManagementProject.Models;

namespace TicketManagementProject.Controllers
{
    public class EventsController : Controller
    {
        private MainDBEntities db = new MainDBEntities();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // POST for searching Event
        [HttpPost]
        public ActionResult Index(string eventName)
        {

            string searchQuery = "%" + eventName + "%";

            MainDBEntities context = new MainDBEntities();
            var events = context.Events.Where(e => e.Name.Contains(eventName)).ToList();


            return View(events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,TicketPrice,EventDate,Address,City,VenueCapacity, EventImg")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,TicketPrice,EventDate,Address,City,VenueCapacity, EventImg")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET for Book Ticket
        public ActionResult BookTicket(int eventId, int userId)
        {
            if (Session["id"] != null)
            {

                var context = new MainDBEntities();
                Event eve = context.Events.Single(e => e.Id == eventId);

                ViewBag.Event = eventId;
                ViewBag.EventName = eve.Name;
                ViewBag.UserId = userId;
                return View("BookTicket");
            }

            return RedirectToAction("Login", "Users");
        }

        //POST for Book Ticket
        [HttpPost]
        public ActionResult BookTicket(FormCollection formCollection)
        {
            var ticket = new Purchase();
            var context = new MainDBEntities();

            ticket.EventId = Convert.ToInt32(formCollection["EventId"]);
            ticket.UserId = Convert.ToInt32(Session["Id"].ToString());
            ticket.PurchaseDate = Convert.ToDateTime(formCollection["PurchaseDate"]);
            ticket.Quantity = Convert.ToInt32(formCollection["Quantity"]);

            context.Purchases.Add(ticket);
            context.SaveChanges();

            return RedirectToAction("Index");
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
