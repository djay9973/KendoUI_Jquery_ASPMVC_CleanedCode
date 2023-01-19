using KendoUI_Jquery_ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUI_Jquery_ASPMVC.Services.Interface
{
   public interface IUploadModule
    {
        IEnumerable<Module> GetModules();
        void Insert(Module module);
        void Update(Module module);
        void Delete(int MID);
        Module GetModulesByID(Module module);
    }
}
