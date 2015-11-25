using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : IDataRepository<Product>
    {
        protected SalesEntityModels.Product ObjectToEntity(Product item)
        {
            return new SalesEntityModels.Product()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        protected Product EntityToObject(SalesEntityModels.Product item)
        {
            return new Product()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public void Add(Product item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Products.Add(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Update(Product item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                var products = context.Products.Find(item.Id);
                context.Entry(products).CurrentValues.SetValues(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Remove(Product item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Products.Remove(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public IList<Product> GetAll(Func<Product, bool> predicate)
        {
            IList<Product> managers;
            using (var context = new SalesEntityModels.SalesDB())
            {
                managers = context
                    .Products
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .Where(predicate).ToList();
            }
            return managers;
        }
    }
}
