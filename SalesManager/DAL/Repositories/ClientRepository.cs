using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientRepository : DataRepository<Client, SalesEntityModels.Client>
    {
        protected SalesEntityModels.Client ObjectToEntity(Client item)
        {
            return new SalesEntityModels.Client()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }

        protected Client EntityToObject(SalesEntityModels.Client item)
        {
            return new Client()
            {
                Id = item.Id,
                LastName = item.LastName
            };
        }
    }
}
