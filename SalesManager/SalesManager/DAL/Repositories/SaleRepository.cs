using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SaleRepository : DataRepository<Sale, EntityModels.Sale>
    {
        protected override EntityModels.Sale ObjectToEntity(Sale item)
        {
            return new EntityModels.Sale()
            {
                Id = item.Id,
                ClientId = item.Client.Id,
                ManagerId = item.Manager.Id,
                ProductId = item.Product.Id,
                Date = item.Date,
                Sum = item.Sum
            };
        }

        protected override Sale EntityToObject(EntityModels.Sale item)
        {
            return new Sale()
            {
                Id = item.Id,
                Client = new Client() { LastName = item.Client.LastName, FirstName = item.Client.FirstName },
                Manager = new Manager() { LastName = item.Manager.LastName },
                Date = item.Date,
                Product = new Product() { Name = item.Product.Name },
                Sum = item.Sum
            };
        }
    }
}
