using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientRepository : IDataRepository<Client>
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

        public void Add(Client item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Clients.Add(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Update(Client item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                var client = context.Clients.Find(item.Id);
                context.Entry(client).CurrentValues.SetValues(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Remove(Client item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Clients.Remove(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public IList<Client> GetAll(Func<Client, bool> predicate)
        {
            IList<Client> clients;
            using (var context = new SalesEntityModels.SalesDB())
            {
                clients = context
                    .Clients
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .Where(predicate).ToList();
            }
            return clients;
        }
    }
}
