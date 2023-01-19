using KendoUI_Jquery_ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUI_Jquery_ASPMVC.Services.Interface
{
    public interface IBookRepository : IDisposable
    {
        IEnumerable<Book> GetBooks();
        void Insert(Book book);
        void Deletes(Book book);
        void Update(Book book);
    }
}
