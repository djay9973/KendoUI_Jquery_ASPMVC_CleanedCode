using KendoUI_Jquery_ASPMVC.Models;
using KendoUI_Jquery_ASPMVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KendoUI_Jquery_ASPMVC.Services.Repository
{
    public class UploadModuleRepository : IUploadModule
    {
        private BooksEntities _context;

        public UploadModuleRepository(BooksEntities bookContext)
        {
            this._context = bookContext;
        }
        public IEnumerable<Module> GetModules()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Modules;
        }
        public void Insert(Module modules)
        {
            Random rand = new Random();
            var tid = rand.Next(0, 1000);

            var ids = GetModules().OrderByDescending(e => e.MID).FirstOrDefault();
            var idd = (ids != null) ? ids.MID : 0;
            modules.MID = idd + 1;
            var entity = new Module();
            entity.MID = modules.MID;
            entity.Moduletitle = modules.Moduletitle;
            entity.CreatedOn =Convert.ToString(DateTime.Today);
            _context.Modules.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Module module)
        {
            var modules = ViewModelToDomain(module);
            _context.Entry(modules).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public Module GetModulesByID(Module module)
        {
            var modules = ViewModelToDomain(module);
            var module1 = new Module
            {
                MID = (int)module.MID,
            };
            Module mod = _context.Modules.Find(module.MID);
            return mod;
        }
        public void Delete(int MID)
        {
            Module module = _context.Modules.Find(MID);
            _context.Modules.Remove(module);
            _context.SaveChanges();
        }

        public Guid ToGuid(Guid? source)
        {
            return source ?? Guid.Empty;
        }

        public Module ViewModelToDomain(Module module)
        {
            Module modules = new Module()
            {
                MID = module.MID,
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