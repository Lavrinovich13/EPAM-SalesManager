using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SaleRepository : DataRepository<Sale, SalesEntityModels.Sale>
    {
        protected override SalesEntityModels.Sale ObjectToEntity(Sale item)
        {
            return new SalesEntityModels.Sale()
            {
                Id = item.Id,
                ClientId = item.ClientId,
                ManagerId = item.ManagerId,
                Date = item.Date,
                ProductId = item.ProductId,
                Sum = item.Sum
            };
        }

        protected override Sale EntityToObject(SalesEntityModels.Sale item)
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
