using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BL.Parser;

namespace BL
{
    class Program
    {
        static void Main(string[] args)
        {
            var fandler = 
                new FileHandler(new CsvParser(), new ManagerRepository(), new ClientRepository(), new ProductRepository(), new SaleRepository());

            var fold = "D:/fold";
            var file = "lavrinovich_22112015.txt";

            fandler.Handle(fold, file);
        }
    }
}
