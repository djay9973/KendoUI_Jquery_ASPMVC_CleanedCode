using KendoUI_Jquery_ASPMVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoUI_Jquery_ASPMVC.Models;
using System.Data.Entity;

namespace KendoUI_Jquery_ASPMVC.Services.Repository
{
    public class BookRepository : IBookRepository
    {
        private BooksEntities _context;

        public BookRepository(BooksEntities bookContext)
        {
            this._context = bookContext;
        }
        public IEnumerable<Book> GetBooks()
        {
            return _context.Books;
        }
        public void Insert(Book book)
        {
            var first = GetBooks().OrderByDescending(e => e.Id).FirstOrDefault();
            var id = (first != null) ? first.Id : 0;
            book.Id = id + 1;

            var entity = new Book();
            entity.Id = book.Id;
            entity.Title = book.Title;
            entity.Authers = book.Authers;

            _context.Books.Add(entity);
            _context.SaveChanges();
        }

        public void InsertBook(Book book)
        {
            _context.Books.Add(book);
        }
        public void Update(Book book)
        {
            var books = ViewModelToDomain(book);
            _context.Entry(books).State = EntityState.Modified;
            _context.SaveChanges();
        }



        public void Deletes(Book book)
        {
             var books = ViewModelToDomain(book);
            var bok = new Book
            {
                Id = (int)books.Id,
            };
            _context.Books.Remove(bok);
            _context.SaveChanges();
        }

        public Guid ToGuid(Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public Book ViewModelToDomain(Book book)
        {
            Book article = new Book()
            {
                Id = book.Id,
                Title = book.Title,
                Authers = book.Authers,
            };

            return article;
        }

        public Book DomainToViewModel(Book book, string Abbreviated)
        {
            Book articleViewModel = new Book
            {
                Id = book.Id,
                Title = book.Title,
                Authers = book.Authers,
            };

            return articleViewModel;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}