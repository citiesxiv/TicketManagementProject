//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketManagementProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}