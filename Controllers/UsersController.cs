using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketManagementProject.Models;


namespace TicketManagementProject.Controllers
{
    public class UsersController : Controller
    {
        MainDBEntities db = new MainDBEntities();
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
            var users = db.Users.Where(e => e.Name.Contains(userName)).ToList();


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

                    Session["id"] = user.Id;
                    Session["name"] = user.Name;
                    Session["email"] = user.Email;
                    Session["address"] = user.Address;
                    Session["userType"] = user.UserType;
                    Session["phoneNumber"] = user.PhoneNumber;

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.error = "Please Leave no fields blank.";
                    return RedirectToAction("Create");
                }

               
        }
           
        
        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        public ActionResult Login(FormCollection loginDetails)
        {
            string email = loginDetails["email"];
            string password = loginDetails["password"];

            try
            {

                var user = db.Users.First(x => (x.Email == email) && (x.Password == password));

                if(user != null)
                {
                    Session["id"] = user.Id;
                    Session["name"] = user.Name;
                    Session["email"] = user.Email;
                    Session["address"] = user.Address;
                    Session["userType"] = user.UserType;
                    Session["phoneNumber"] = user.PhoneNumber;
                    return RedirectToAction("Index", "Home");
                
                }

            } catch
            {
                return View("Login");
            }

            return View();

        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return View();
        }
           
        

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            var user = db.Users.Single(u => u.Id == id);
            ViewBag.UserName = user.Name;
            ViewBag.UserPhone = user.PhoneNumber;
            ViewBag.UserEmail = user.Email;
            ViewBag.UserAddress = user.Address;
            ViewBag.UserType = user.UserType;
            ViewBag.UserPW = user.Password;
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event events)
        {
            try
            {
                MainDBEntities dbEntities = new MainDBEntities();
                dbEntities.Entry(events).State = System.Data.Entity.EntityState.Modified;
                dbEntities.SaveChanges();

                return RedirectToAction("Index", "Home");
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

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // Get User's Profile
        public ActionResult Profile()
        {
            if (Session["id"] != null)
            {
                var userId = Convert.ToInt32(Session["id"].ToString());
                var user = db.Users.First(x => x.Id == userId);

                return View(user);
            }
               
            return RedirectToAction("Login");
        }

        public ActionResult Admin()
        {
            var users = db.Users.ToList();
            ViewBag.Events = db.Events.ToList();
            ViewBag.Purchases = db.Purchases.ToList();
            if(Session["userType"] == null)
            {
                ViewBag.SessionType = "NA";
            }else
            {
                ViewBag.SessionType = Session["userType"].ToString();
            }

            return View(users);
        }

    }
}
