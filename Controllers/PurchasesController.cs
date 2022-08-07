using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketManagementProject.Models;

namespace TicketManagementProject.Controllers
{
    public class PurchasesController : Controller
    {
        // GET: Purchases
        // Get User's Purchased Tickets
        public ActionResult _PurchasedTickets()
        {

            var userId = Convert.ToInt32(Session["id"].ToString());

            using (MainDBEntities dbEntities = new MainDBEntities())
            {
                List<Event> events = dbEntities.Events.ToList();
                List<Purchase> purchases = dbEntities.Purchases.ToList();

                try
                {
                    var purchaseRecord = from p in purchases
                                         join e in events on p.EventId equals e.Id into table1
                                         from e in table1.ToList()
                                         where p.UserId == userId
                                         select new PurchaseViewModel
                                         {
                                             events = e,
                                             purchase = p
                                         };

                    return PartialView(purchaseRecord);
                }
                catch
                {
                    return View("Index", "Events");
                }

            }
        }
    }

    
}