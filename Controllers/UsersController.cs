using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketManagementProject.Models;


namespace TicketManagementProject.Controllers
{
    public class UsersController : Controller
    {
        private MainDBEntities db = new MainDBEntities();
        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public ActionResult Index(string userName)
        {
            string searchQuery = "%" + userName + "%";

            MainDBEntities context = new MainDBEntities();
            var users = context.Users.Where(e => e.Name.Contains(userName)).ToList();

            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            
                // 
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index","Events");
                }
                else
                {
                    ViewBag.error = "Please Leave no fields blank.";
                    return RedirectToAction("Create");
                }

                
        }
           
        

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event events)
        {
            try
            {
                // TODO: Add update logic hereI'm
                MainDBEntities dbEntities = new MainDBEntities();
                dbEntities.Entry(events).State = System.Data.Entity.EntityState.Modified;
                dbEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
