using KendoUI_Jquery_ASPMVC.Models;
using KendoUI_Jquery_ASPMVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoUI_Jquery_ASPMVC.Services.Repository
{
    public class UserHelpRepository : IUserhelpRepository
    {
        private BooksEntities _context;

        public UserHelpRepository(BooksEntities bookContext)
        {
            this._context = bookContext;
        }
        public IEnumerable<Ticket> GetFiles()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Tickets;
        }
        public void Insert(string Title)
        {
            var entity = new Ticket();
            var ids = GetFiles().OrderByDescending(e => e.Id).FirstOrDefault();
            var tids = GetFiles().OrderByDescending(e => e.TicketNumber).FirstOrDefault();
            if (ids == null && tids == null)
            {
                entity.Id = 1;
                entity.TicketNumber = 1;
                entity.Title = Title;
                entity.Status = "Active";
                entity.CreatedOn = DateTime.Today.ToString();
                _context.Tickets.Add(entity);
                _context.SaveChanges();
            }
            else
            {
                var idd = (ids != null) ? ids.Id : 0;
                var tidd = (tids != null) ? tids.TicketNumber : 0;
                entity.Id = idd + 1;
                entity.TicketNumber = tidd + 1;
                entity.Title = Title;
                entity.Status = "Active";
                entity.CreatedOn = DateTime.Today.ToString();
                _context.Tickets.Add(entity);
                _context.SaveChanges();
            }
        }
    }
}