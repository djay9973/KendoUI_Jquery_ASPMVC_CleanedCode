using KendoUI_Jquery_ASPMVC.Models;
using KendoUI_Jquery_ASPMVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KendoUI_Jquery_ASPMVC.Services.Repository
{
    public class VideoRepository : IUploadRepository
    {
        private BooksEntities _context;

        public VideoRepository(BooksEntities bookContext)
        {
            this._context = bookContext;
        }
       
        public IEnumerable<VideoFile> GetVideoes()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var data= _context.VideoFiles;
            if(data!= null)
            {
                return data;
            }
            else
            {
                return null;
            }
             
        }
       public string SaveUploadedFile(string Titles, string Filename, string filePaths,int ModuleId)
        {
            VideoFile videos = new VideoFile();
             videos.Title = Titles;
             videos.ModuleId = ModuleId;
             var pth= Path.Combine(filePaths, Filename).Replace(@"\", "/");
             videos.FilePath = pth;
             videos.FileName = Filename;


            _context.VideoFiles.Add(videos);
            _context.SaveChanges();
            return videos.FilePath;
        }
       public VideoFile ViewModelToDomain(VideoFile video)
        {
            VideoFile videoes = new VideoFile()
            {
                ID = video.ID,
                Title = video.Title,
                FilePath = video.FilePath,
                ModuleId=video.ModuleId,
                FileName=video.FileName,
            };

            return video;
        }
        public void UpdateVideo(VideoFile video)
        {
            var videoes = ViewModelToDomain(video);
            _context.Entry(video).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteVideo(int id)
        {
            VideoFile video = _context.VideoFiles.Find(id);
            _context.VideoFiles.Remove(video);
            _context.SaveChanges();
        }
        public VideoFile DownloadAttachment(int Id)
        {
            try
            {
                var data = _context.VideoFiles.Where(x => x.ID == Id).Select(c => new VideoFile
                {
                    Title = c.Title,
                }).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}