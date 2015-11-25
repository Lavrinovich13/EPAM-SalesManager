using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ManagerRepository : DataRepository<Manager, SalesEntityModels.Manager>
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
    }
}
