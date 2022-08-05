using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketManagementProject.Models
{
    public class PurchaseViewModel
    {
        public Event events { get; set; }
        public Purchase purchase { get; set; }
    }
}