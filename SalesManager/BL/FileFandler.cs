using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using System.IO;
using System.Transactions;
using BL.Models.Interfaces;

namespace BL
{
    public class FileHandler
    {
        protected IParser _parser;

        protected IDataRepository<Manager> _managerRepository;
        protected IDataRepository<Client> _clientRepository;
        protected IDataRepository<Product> _productRepository;
        protected IDataRepository<Sale> _saleRepository;

        public FileHandler(IParser parser, 
            IDataRepository<Manager> managerRepository,
            IDataRepository<Client> clientRepository,
            IDataRepository<Product> productRepository,
            IDataRepository<Sale> saleRepository)
        {
            _parser = parser;
            _clientRepository = clientRepository;
            _managerRepository = managerRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
        }

        public void Handle(string folder, string fileName)
        {
            var title = _parser.ParseTitle(fileName);

            var manager = GetFromRepository<Manager>
                (new Manager() { LastName = title.ManagerLastName }, _managerRepository);

            using (var transaction = new TransactionScope())
            {
                string d = Path.Combine(folder, fileName);
                foreach (var line in ReadLines(Path.Combine(new[] {folder, fileName})))
                {
                    var record = _parser.ParseRecord(line);

                    var client = GetFromRepository<Client>
                        (new Client() { LastName = record.ClientLastName }, _clientRepository);

                    var product = GetFromRepository<Product>
                        (new Product() { Name = record.ProductName }, _productRepository);

                    var sale = new Sale()
                      {
                          ManagerId = manager.Id,
                          ClientId = 1,
                          ProductId = product.Id,
                          Date = record.Date,
                          Sum = record.Sum
                      };

                    _saleRepository.Add(sale);
                }

                transaction.Complete();
            }
        }

        protected T GetFromRepository<T>(T item, IDataRepository<T> repository) 
            where T : class
        {
            if (repository.GetIfExists(item) == null)
            {
                lock (repository)
                {
                    repository.Add(item);
                }
            }

            return repository.GetIfExists(item);
        }

        protected IEnumerable<string> ReadLines(string path)
        {
            using(var reader = new StreamReader(path))
            {
                string currentString;
                while((currentString = reader.ReadLine())!=null)
                {
                    yield return currentString;
                }
            }
        }
    }
}
