using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SalesRepository : DataRepository<Sale, SalesEntityModels.Sale>
    {
        protected SalesEntityModels.Sale ObjectToEntity(Sale item)
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

        protected Sale EntityToObject(SalesEntityModels.Sale item)
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
