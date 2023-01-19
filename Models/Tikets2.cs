using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoUI_Jquery_ASPMVC.Models
{
    public class Tikets2
    {
        public int Id { get; set; }
        public int TicketNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ClosedAt { get; set; }
        public string ClosedBy { get; set; }
       /* public Nullable<int> UserId { get; set; }
        public Nullable<int> RoleId { get; set; }

        public virtual Role Role { get; set; }*/
    }
}