using KendoUI_Jquery_ASPMVC.Services.Interface;
using KendoUI_Jquery_ASPMVC.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoUI_Jquery_ASPMVC.Models;
using System.Data;

namespace KendoUI_Jquery_ASPMVC.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;
        public BookController()
        {
            this._bookRepository = new BookRepository(new BooksEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReadBooks()
        {
            var books = from book in _bookRepository.GetBooks()
                        select book;
            return Json(books,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                if (book != null)
                {
                    _bookRepository.Insert(book);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(new[] { book });
        }
       
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bookRepository.Update(book);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(new[] { book });
        }
        public ActionResult Delete(Book book)
        {
            _bookRepository.Deletes(book);
            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
}