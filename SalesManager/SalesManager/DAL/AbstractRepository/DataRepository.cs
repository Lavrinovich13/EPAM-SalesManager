using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AbstractRepository
{
    public abstract class DataRepository<T,K> : IDataRepository<T>
        where T : class, IEquatable<T>
        where K : class
    {
        protected abstract K ObjectToEntity(T item);

        protected abstract T EntityToObject(K item);

        public virtual void Add(T item)
        {
            using (var context = new EntityModels.SalesDBEntities())
            {
                context.Entry(ObjectToEntity(item)).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (var context = new EntityModels.SalesDBEntities())
            {
                context.Entry(ObjectToEntity(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual void Remove(T item)
        {
            using (var context = new EntityModels.SalesDBEntities())
            {
                context.Entry(ObjectToEntity(item)).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            IEnumerable<T> list;
            using (var context = new EntityModels.SalesDBEntities())
            {
                list = context
                    .Set<K>()
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .Where(predicate)
                    .ToList();
            }
            return list ?? new List<T>();
        }

        public virtual T GetIfExists(T item)
        {
            T dbItem;
            using (var context = new EntityModels.SalesDBEntities())
            {
                var entityItem = context
                    .Set<K>()
                    .ToList()
                    .Select(x => EntityToObject(x))
                    .FirstOrDefault(x => x.Equals(item));
                dbItem = entityItem;
            }
            return dbItem;
        }
    }
}
