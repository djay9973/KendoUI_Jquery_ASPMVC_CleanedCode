using KendoUI_Jquery_ASPMVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoUI_Jquery_ASPMVC.Models;
using System.Data.Entity;

namespace KendoUI_Jquery_ASPMVC.Services.Repository
{
    public class HelpRepository : IHelpRepository
    {
        private BooksEntities _context;

        public HelpRepository(BooksEntities bookContext)
        {
            this._context = bookContext;
        }
        public IEnumerable<Ticket> GetTickets()
        {
            var data = _context.Tickets;
            if (data!= null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }
        public void Insert(Ticket tickets)
        {
            try
            {
                Random rand = new Random();
                var tid = rand.Next(0, 1000);

                var entity = new Ticket();
                var first = GetTickets().OrderByDescending(e => e.TicketNumber).FirstOrDefault();
                var ids = GetTickets().OrderByDescending(e => e.Id).FirstOrDefault();
                if (first == null && ids == null)
                {
                    entity.Id = 1;
                    entity.TicketNumber = 1;
                    entity.Title = tickets.Title;
                    entity.Description = tickets.Description;
                    entity.Status = tickets.Status;
                    entity.CreatedOn = DateTime.Now.ToString();
                    entity.ClosedAt = tickets.ClosedAt.ToString();
                    _context.Tickets.Add(entity);
                    _context.SaveChanges();
                }
                else
                {
                    var id = (first != null) ? first.TicketNumber : 0;
                    tickets.TicketNumber = id + 1;
                    var idd = (ids != null) ? first.Id : 0;
                    tickets.Id = idd + 1;
                    entity.Id = tickets.Id;
                    entity.TicketNumber = tickets.TicketNumber;
                    entity.Title = tickets.Title;
                    entity.Description = tickets.Description;
                    entity.Status = tickets.Status;
                    entity.CreatedOn = DateTime.UtcNow.Date.ToString();
                    entity.ClosedAt = tickets.ClosedAt.ToString();
                    _context.Tickets.Add(entity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void Update(Ticket tickets)
        {
            var ticket = ViewModelToDomain(tickets);
            _context.Entry(ticket).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public Ticket GetTicketsByID(Ticket tickets)
        {
            var ticket = ViewModelToDomain(tickets);
            var bok = new Ticket
            {
                Id = (int)ticket.Id,
            };
            Ticket tickt = _context.Tickets.Find(ticket.Id);
            return tickt;
        }
        public void Delete(int id)
        {
            Ticket tickt = _context.Tickets.Find(id);
            _context.Tickets.Remove(tickt);
            _context.SaveChanges();
        }

        public Guid ToGuid(Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public Ticket ViewModelToDomain(Ticket tickets)
        {
            Ticket article = new Ticket()
            {
                Id = tickets.Id,
                TicketNumber=tickets.TicketNumber,
                Title = tickets.Title,
                Description = tickets.Description,
                Status= tickets.Status,
                CreatedOn = tickets.CreatedOn,
                CreatedBy = tickets.CreatedBy,
                ClosedAt = tickets.ClosedAt,
                ClosedBy = tickets.ClosedBy,
            };

            return article;
        }

        public Ticket DomainToViewModel(Ticket tickets, string Abbreviated)
        {
            Ticket articleViewModel = new Ticket
            {
                Id = tickets.Id,
                Title = tickets.Title,
                Description = tickets.Description,
                Status = tickets.Description,
                CreatedOn = tickets.CreatedOn,
                CreatedBy = tickets.CreatedBy,
                ClosedAt = tickets.ClosedAt,
                ClosedBy = tickets.ClosedBy,
            };

            return articleViewModel;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}