using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ManagerRepository : DataRepository<Manager, EntityModels.Manager>
    {
        protected override EntityModels.Manager ObjectToEntity(Manager item)
        {
            return new EntityModels.Manager()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }

        protected override Manager EntityToObject(EntityModels.Manager item)
        {
            return new Manager()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }
    }
}
