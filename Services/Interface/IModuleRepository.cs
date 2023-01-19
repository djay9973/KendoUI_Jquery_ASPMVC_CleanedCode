using KendoUI_Jquery_ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUI_Jquery_ASPMVC.Services.Interface
{
   public interface IModuleRepository
    {
        IEnumerable<ModuleList> GetModules();
        void Insert(ModuleList module);
        void Update(ModuleList module);
        void Delete(int ModuleId);
        ModuleList GetModulesByID(ModuleList module);
    }
}
