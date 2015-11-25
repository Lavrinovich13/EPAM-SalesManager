using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ManagerRepository : IDataRepository<Manager>
    {
        protected SalesEntityModels.Manager ObjectToEntity(Manager item)
        {
            return new SalesEntityModels.Manager()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }

        protected Manager EntityToObject(SalesEntityModels.Manager item)
        {
            return new Manager()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }

        public void Add(Manager item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Managers.Add(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Update(Manager item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                var manager = context.Managers.Find(item.Id);
                context.Entry(manager).CurrentValues.SetValues(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Remove(Manager item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Managers.Remove(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public IList<Manager> GetAll(Func<Manager, bool> predicate)
        {
            IList<Manager> managers;
            using (var context = new SalesEntityModels.SalesDB())
            {
                managers = context
                    .Managers
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .Where(predicate).ToList();
            }
            return managers;
        }
    }
}
