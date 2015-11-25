using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SalesRepository : IDataRepository<Sale>
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

        public void Add(Sale item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Sales.Add(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Update(Sale item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                var sale = context.Sales.Find(item.Id);
                context.Entry(sale).CurrentValues.SetValues(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public void Remove(Sale item)
        {
            using (var context = new SalesEntityModels.SalesDB())
            {
                context.Sales.Remove(ObjectToEntity(item));
                context.SaveChanges();
            }
        }

        public IList<Sale> GetAll(Func<Sale, bool> predicate)
        {
            IList<Sale> sales;
            using (var context = new SalesEntityModels.SalesDB())
            {
                sales = context
                    .Sales
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .Where(predicate).ToList();
            }
            return sales;
        }
    }
}
