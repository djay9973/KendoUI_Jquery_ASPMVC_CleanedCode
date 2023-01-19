using KendoUI_Jquery_ASPMVC.Services.Interface;
using KendoUI_Jquery_ASPMVC.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoUI_Jquery_ASPMVC.Models;
using System.Data;
using System.IO;
using System.Security.Principal;

namespace KendoUI_Jquery_ASPMVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class HelpController : Controller
    {
        private IHelpRepository _helpRepository;
        private IUploadRepository _uploadRepository;
        private BooksEntities _context;
        private const string contentFolderRoot = "~/VideoFiles/";
        string CompanyName = "";
        string filename_upload = "";
        public HelpController()
        {
            this._helpRepository = new HelpRepository(new BooksEntities());
            this._uploadRepository = new VideoRepository(new BooksEntities());
        }
        #region Help Tab
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTickets()
        {
            var tickts = from Ticket in _helpRepository.GetTickets()
                        select Ticket;
            return Json(tickts, JsonRequestBehavior.AllowGet);
        }
        public ViewResult Details(Ticket tickets)
        {
            Ticket tickts = _helpRepository.GetTicketsByID(tickets);
            return View(tickts);
        }

        [HttpPost]
        public ActionResult Create(Ticket tickets)
        {
            try
            {
                if (tickets != null)
                {
                    _helpRepository.Insert(tickets);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(new[] { tickets });
        }

        [HttpPost]
        public ActionResult Edit(Ticket tickets)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _helpRepository.Update(tickets);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(tickets);
        }
        public ActionResult Delete(int id)
        {
            _helpRepository.Delete(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion
       
        

        
    }
}