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
using BL.Reader.Interfaces;
using BL.Reader;
using BL.Parser;
using DAL.Repositories;

namespace BL
{
    public class FileHandler
    {
        protected IParser _parser;
        protected IReader _reader;

        protected IDataRepository<Manager> _managerRepository;
        protected IDataRepository<Client> _clientRepository;
        protected IDataRepository<Product> _productRepository;
        protected IDataRepository<Sale> _saleRepository;
        protected IDataRepository<ProcessedReport> _processedReportsRepository;

        public FileHandler()
        {
            _reader = new LineByLineReader();
            _parser = new CsvParser();

            _clientRepository = new ClientRepository();
            _managerRepository = new ManagerRepository();
            _productRepository = new ProductRepository();
            _saleRepository = new SaleRepository();
            _processedReportsRepository = new ProcessedReportRepository();
        }

        protected bool IsFileAlreadyProcess(string filename)
        {
            return _processedReportsRepository
                .FindByFields(new ProcessedReport() { FileName = filename }) == null ? false : true;
        }

        public void Handle(string folder, string fileName)
        {
            if (IsFileAlreadyProcess(fileName))
                throw new ArgumentException("File was already process");

            var title = _parser.ParseTitle(fileName);

            var manager = GetOrCreateAndGet<Manager>
                (new Manager() { LastName = title.ManagerLastName }, _managerRepository);

            using (var transaction = new TransactionScope())
            {
                foreach (var line in _reader.Read(Path.Combine(new[] { folder, fileName })))
                {
                    var record = _parser.ParseRecord(line);

                    var client = GetOrCreateAndGet<Client>
                        (new Client() { LastName = record.ClientLastName, FirstName = record.ClientFirstName }, _clientRepository);

                    var product = GetOrCreateAndGet<Product>
                        (new Product() { Name = record.ProductName }, _productRepository);

                    var sale = new Sale()
                      {
                          Manager = manager,
                          Client = client,
                          Product = product,
                          Date = record.Date,
                          Sum = record.Sum
                      };


                   // Console.WriteLine("Add sale " + fileName);
                    _saleRepository.Add(sale);
                }
                _processedReportsRepository.Add(new ProcessedReport() { FileName = fileName });
                transaction.Complete();
            }
        }

        protected T GetOrCreateAndGet<T>(T item, IDataRepository<T> repository)
            where T : class
        {
            T buffItem = repository.FindByFields(item);

            if (buffItem != null)
                return buffItem;

            lock (repository)
            {
                repository.Add(item);
                buffItem = repository.FindByFields(item);
            }
            return buffItem;
        }
    }
}
