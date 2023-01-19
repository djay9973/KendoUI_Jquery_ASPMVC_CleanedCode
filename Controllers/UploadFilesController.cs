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
    public class UploadFilesController : Controller
    {
        private IUploadRepository _uploadRepository;
        private BooksEntities _context;
        private const string contentFolderRoot = "~/VideoFiles/";
        string CompanyName = "";
        string filename_upload = "";
        public UploadFilesController()
        {
            this._uploadRepository = new VideoRepository(new BooksEntities());
        }
        #region Video upload Tab
        public ActionResult VideoList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Videos()
        {
            var videos = from VideoFile in _uploadRepository.GetVideoes()
                         select VideoFile;
            return Json(videos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadVideo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(string moduleid)
        {
            try
            {
                var ModuleId = Convert.ToInt32(moduleid);
                _context = new BooksEntities();
                var Modules = (from c in _context.Modules
                               where c.MID == ModuleId
                               select c.Moduletitle).FirstOrDefault();
                HttpFileCollectionBase files = Request.Files;
                List<HttpPostedFileBase> fi = new List<HttpPostedFileBase>();
                string Filename = "", extension = "", tempName = "", tempPath = "", filenames = "";
                var result = "";
                var filePaths = "";
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase filed = files[i];
                        fi.Add(filed);
                        result = result + "FileAdded";
                    }
                    foreach (var filed in fi)
                    {
                        Filename = filed.FileName;
                        filePaths = Path.Combine(contentFolderRoot, Modules).Replace(@"\", "/");
                        var path = Server.MapPath(filePaths);

                        if (!Directory.Exists(path))
                        {
                            DirectoryInfo d = Directory.CreateDirectory(path);
                            System.Security.AccessControl.DirectorySecurity dSecurity = d.GetAccessControl();
                            dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.InheritanceFlags.ObjectInherit | System.Security.AccessControl.InheritanceFlags.ContainerInherit, System.Security.AccessControl.PropagationFlags.NoPropagateInherit, System.Security.AccessControl.AccessControlType.Allow));
                            d.SetAccessControl(dSecurity);
                        }
                        extension = Path.GetExtension(filed.FileName);
                        filenames = Path.GetFileNameWithoutExtension(filed.FileName);
                        int sequence = 0;
                        if (Filename != null)
                        {
                            while (System.IO.File.Exists(filePaths))
                            {
                                Filename = filenames + String.Format("({0})", ++sequence) + extension;
                                path = Server.MapPath(filePaths).Replace(@"\", "/");
                            }
                        }
                        filePaths = filePaths.Replace("~", "..");
                        var path1 = Path.Combine(Server.MapPath(filePaths).Replace("~", ".."), Filename).Replace(@"\", "/");
                        var path2 = Path.Combine(filePaths).Replace('\\', '/');
                        filed.SaveAs(path1);
                        TempData["FileNm"] = Filename;
                        return Json(path1, "text/plain");
                    }
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Upload(string path, HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            string extension = "", fname = "";
            fname = Path.GetFileNameWithoutExtension(file.FileName);
            extension = Path.GetExtension(file.FileName);
            if (path.EndsWith(".exe") || path.EndsWith(".dll") || path.EndsWith(".pdf") || path.EndsWith(".eps") || path.EndsWith(".svg") || path.EndsWith(".js") || path.EndsWith(".cs") || path.EndsWith(".zip"))
            {
                throw new HttpException(500, "You cannot upload invalid files to this folder");
            }
            else
            {
                path = path.Replace("~", "..");
                var path1 = Path.Combine(Server.MapPath(path).Replace("~", ".."), fileName).Replace(@"\", "/");
                var path2 = Path.Combine(path).Replace('\\', '/');
                file.SaveAs(path1);
                return Json(path1, "text/plain");
                throw new HttpException(403, "Forbidden");
            }
        }
        [HttpPost]
        public ActionResult UploadVideos()
        {
            try
            {
                var Form = Request.Form;
                var Moduleid = Convert.ToInt32(Form.Get("ModuleId"));
                var Titles = Form.Get("Title");
                ViewBag.Filenames = TempData["FileNm"];
                string Filename = ViewBag.Filenames;
                _context = new BooksEntities();
                var Modulename = (from c in _context.Modules
                                  where c.MID == Moduleid
                                  select c.Moduletitle).FirstOrDefault();
                string extension = "", tempName = "", tempPath = "", filenames = "";
                var data = "";
                var result = "";
                var filePaths = "";
                filePaths = Path.Combine(contentFolderRoot, Modulename).Replace(@"\", "/");
                var path = Server.MapPath(filePaths);

                if (!Directory.Exists(path))
                {
                    DirectoryInfo d = Directory.CreateDirectory(path);
                    System.Security.AccessControl.DirectorySecurity dSecurity = d.GetAccessControl();
                    dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.InheritanceFlags.ObjectInherit | System.Security.AccessControl.InheritanceFlags.ContainerInherit, System.Security.AccessControl.PropagationFlags.NoPropagateInherit, System.Security.AccessControl.AccessControlType.Allow));
                    d.SetAccessControl(dSecurity);
                }
                extension = Path.GetExtension(Filename);
                filenames = Path.GetFileNameWithoutExtension(Filename);
                int sequence = 0;
                if (Filename != null)
                {
                    while (System.IO.File.Exists(filePaths))
                    {
                        Filename = filenames + String.Format("({0})", ++sequence) + extension;
                        path = Server.MapPath(filePaths).Replace(@"\", "/");
                    }
                }
                data = _uploadRepository.SaveUploadedFile(Titles ,Filename, filePaths, Moduleid);
                result = data;
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Edit(VideoFile videos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _uploadRepository.UpdateVideo(videos);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Json(videos);
        }
        public ActionResult DeleteVideos(int id)
        {
            _uploadRepository.DeleteVideo(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}