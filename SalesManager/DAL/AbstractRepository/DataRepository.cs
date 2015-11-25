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

        public void Add(T item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Entry(ObjectToEntity(item)).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(T item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Entry(ObjectToEntity(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(T item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Entry(ObjectToEntity(item)).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IList<T> GetAll(Func<T, bool> predicate)
        {
            var list = new List<T>();
            using (var context = new SalesEntityModels.SalesDB())
            {
                list = context
                    .Set<K>()
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .Where(predicate)
                    .ToList();
            }
            return list;
        }

        public T GetIfExists(T item)
        {
            T dbItem;
            using (var context = new SalesEntityModels.SalesDB())
            {
                var entityItem = context.Set<K>().ToList().Select(x => EntityToObject(x)).FirstOrDefault(x => x.Equals(item));
                dbItem = entityItem;
            }
            return dbItem;
        }
    }
}
