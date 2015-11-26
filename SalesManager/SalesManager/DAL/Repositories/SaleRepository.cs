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
                ClientId = item.ClientId,
                ManagerId = item.ManagerId,
                Date = item.Date,
                ProductId = item.ProductId,
                Sum = item.Sum
            };
        }

        protected override Sale EntityToObject(EntityModels.Sale item)
        {
            return new Sale()
            {
                Id = item.Id,
                ClientId = item.ClientId,
                ManagerId = item.ManagerId,
                Date = item.Date,
                ProductId = item.ProductId,
                Sum = item.Sum
            };
        }
    }
}
