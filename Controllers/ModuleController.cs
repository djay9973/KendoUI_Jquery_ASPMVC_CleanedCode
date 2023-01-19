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
    public class ModuleController : Controller
    {
        private IModuleRepository _moduleRepository;
        private BooksEntities _context;
        public ModuleController()
        {
            this._moduleRepository = new ModulesRepository(new BooksEntities());
        }
        #region Modules Tab
        public ActionResult Moduls()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Moduless()
        {
            var module = from ModuleList in _moduleRepository.GetModules()
                         select ModuleList;
            return Json(module, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Modls()
        {
            var module = (from ModuleList in _moduleRepository.GetModules()
                          select new
                          {
                              ModuleList.ModuleId,
                              ModuleList.Moduletitle
                          }).ToList();
            return Json(module, JsonRequestBehavior.AllowGet);
        }
        public ViewResult Details(ModuleList module)
        {
            ModuleList modules = _moduleRepository.GetModulesByID(module);
            return View(modules);
        }

        [HttpPost]
        public ActionResult CreateModule(ModuleList module)
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
        public ActionResult EditModule(ModuleList module)
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
        public ActionResult Delete(int ModuleId)
        {
             _moduleRepository.Delete(ModuleId);
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}