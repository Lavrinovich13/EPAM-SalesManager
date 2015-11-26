using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientRepository : DataRepository<Client, EntityModels.Client>
    {
        protected override EntityModels.Client ObjectToEntity(Client item)
        {
            return new EntityModels.Client()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }

        protected override Client EntityToObject(EntityModels.Client item)
        {
            return new Client()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }
    }
}
