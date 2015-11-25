using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : DataRepository<Product, SalesEntityModels.Product>
    {
        protected override SalesEntityModels.Product ObjectToEntity(Product item)
        {
            return new SalesEntityModels.Product()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        protected override Product EntityToObject(SalesEntityModels.Product item)
        {
            return new Product()
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}
