using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IDataRepository<T>
        where T : class
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        IList<T> GetAll(Func<T, bool> predicate);
    }
}
