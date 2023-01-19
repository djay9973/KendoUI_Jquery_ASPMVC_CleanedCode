using KendoUI_Jquery_ASPMVC.Models;
using KendoUI_Jquery_ASPMVC.Services.Interface;
using KendoUI_Jquery_ASPMVC.Services.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoUI_Jquery_ASPMVC.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class UserHelpController : Controller
    {
        private IUserhelpRepository _userRepository;
        private BooksEntities _context;
        public UserHelpController()
        {
            this._userRepository = new UserHelpRepository(new BooksEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddQuery(string Title)
        {
            try
            {

                if (Title != null)
                {
                   _userRepository.Insert(Title);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(Title);
        }
        [HttpGet]
        public JsonResult GetAllFiles(string Title)
        {
            if (Title != null)
            {
                _context = new BooksEntities();
                var Modulenm = (from c in _context.Modules
                               where c.Moduletitle == Title
                                select c.MID).FirstOrDefault();
                 var files = _context.VideoFiles.Where(x => x.ModuleId == Modulenm).Select(ms => new
                 {
                     Title = ms.Title,
                     FilePath = ms.FilePath,
                     ModuleId = ms.ModuleId
                 }).FirstOrDefault();
                return Json(files, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
        public VideoFile ViewModelToDomain(VideoFile video)
        {
            VideoFile videoes = new VideoFile()
            {
                ID = video.ID,
                Title = video.Title,
                FilePath = video.FilePath,
                ModuleId = video.ModuleId,
                FileName=video.FileName,
            };

            return video;
        }
        public FileResult DownloadFile(string FilePaths)
        {
            {
                _context = new BooksEntities();
                var filename = (from c in _context.VideoFiles
                                where c.FilePath == FilePaths
                                select c.FileName).FirstOrDefault();
                byte[] fileBytes = GetFile(Server.MapPath(FilePaths));
                var mm = MimeMapping.GetMimeMapping(filename);
                return File(
                    fileBytes, mm, filename);
            }

            byte[] GetFile(string s)
            {
                System.IO.FileStream fs = System.IO.File.OpenRead(s);
                byte[] data = new byte[fs.Length];
                int br = fs.Read(data, 0, data.Length);
                if (br != fs.Length)
                    throw new System.IO.IOException(s);
                return data;
            }
        }
    }
}