using KendoUI_Jquery_ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KendoUI_Jquery_ASPMVC.Services.Interface
{
   public interface IUploadRepository
    {
        IEnumerable<VideoFile> GetVideoes();
        void UpdateVideo(VideoFile video);
        void DeleteVideo(int id);
        string SaveUploadedFile(string Titles, string Filename, string filePaths, int ModuleId);
        VideoFile DownloadAttachment(int Id);
    }
}
