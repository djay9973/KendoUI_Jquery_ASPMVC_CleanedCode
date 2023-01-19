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
    public class UploadModuleController : Controller
    {
        private IUploadModule _moduleRepository;
        private BooksEntities _context;
        public UploadModuleController()
        {
            this._moduleRepository = new UploadModuleRepository(new BooksEntities());
        }
        #region Modules Tab
        public ActionResult Moduls()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Moduless()
        {
            var module = from Module in _moduleRepository.GetModules()
                         select Module;
            return Json(module, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Modls()
        {
            var module = (from Module in _moduleRepository.GetModules()
                          select new
                          {
                              Module.MID,
                              Module.Moduletitle
                          }).ToList();
            return Json(module, JsonRequestBehavior.AllowGet);
        }
        public ViewResult Details(Module module)
        {
            Module modules = _moduleRepository.GetModulesByID(module);
            return View(modules);
        }

        [HttpPost]
        public ActionResult CreateModule(Module module)
        {
            try
            {
                if (module != null)
                {
                    _moduleRepository.Insert(module);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(new[] { module });
        }

        [HttpPost]
        public ActionResult Edit(Module module)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _moduleRepository.Update(module);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(module);
        }
        public ActionResult Delete(int MID)
        {
            _moduleRepository.Delete(MID);
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}