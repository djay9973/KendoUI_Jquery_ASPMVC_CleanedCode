using KendoUI_Jquery_ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUI_Jquery_ASPMVC.Services.Interface
{
   public interface IHelpRepository : IDisposable
    {
        IEnumerable<Ticket> GetTickets();
        void Insert(Ticket tickets);
        void Update(Ticket tickets);
        void Delete(int id);
        Ticket GetTicketsByID(Ticket tickets);
    }
}
