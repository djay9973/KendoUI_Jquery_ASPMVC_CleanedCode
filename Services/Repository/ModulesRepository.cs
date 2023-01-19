using KendoUI_Jquery_ASPMVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoUI_Jquery_ASPMVC.Models;
using System.Data.Entity;

namespace KendoUI_Jquery_ASPMVC.Services.Repository
{
    public class ModulesRepository : IModuleRepository
    {
        private BooksEntities _context;

        public ModulesRepository(BooksEntities bookContext)
        {
            this._context = bookContext;
        }
        public IEnumerable<ModuleList> GetModules()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.ModuleLists ;
        }
        public void Insert(ModuleList modules)
        {
            Random rand = new Random();
            var tid = rand.Next(0, 1000);

            var ids = GetModules().OrderByDescending(e => e.ModuleId).FirstOrDefault();
            var idd = (ids != null) ? ids.ModuleId : 0;
            modules.ModuleId = idd + 1;
            var entity = new ModuleList();
            entity.ModuleId = modules.ModuleId;
            entity.Moduletitle = modules.Moduletitle;
            entity.CreatedOn = DateTime.Today;
            _context.ModuleLists.Add(entity);
            _context.SaveChanges();
        }

        public void Update(ModuleList module)
        {
            var modules = ViewModelToDomain(module);
            _context.Entry(modules).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public ModuleList GetModulesByID(ModuleList module)
        {
            var modules = ViewModelToDomain(module);
            var module1 = new ModuleList
            {
                ModuleId = (int)module.ModuleId,
            };
            ModuleList mod = _context.ModuleLists.Find(module.ModuleId);
            return mod;
        }
        public void Delete(int ModuleId)
        {
            ModuleList module = _context.ModuleLists.Find(ModuleId);
            _context.ModuleLists.Remove(module);
            _context.SaveChanges();
        }

        public Guid ToGuid(Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public ModuleList ViewModelToDomain(ModuleList module)
        {
            ModuleList modules = new ModuleList()
            {
                ModuleId = module.ModuleId,
                Moduletitle = module.Moduletitle,
                CreatedOn = module.CreatedOn,
                CreatedBy = module.CreatedBy,
            };

            return modules;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}