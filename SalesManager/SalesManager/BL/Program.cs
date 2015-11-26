using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BL.Parser;
using BL.Scheduler;
using System.Threading;

namespace BL
{
    class Program
    {
        static void Main(string[] args)
        {
            var fandler = 
                new FileHandler(new CsvParser(), new ManagerRepository(), new ClientRepository(), new ProductRepository(), new SaleRepository());

            var fold = "D:/fold";
            var file1 = "lavrinovich_22112015.csv";
            var file2 = "lavrinovich_23112015.csv";


            LimitedTaskScheduler sch = new LimitedTaskScheduler(1);
            TaskFactory factory = new TaskFactory(sch);
            CancellationTokenSource cts = new CancellationTokenSource();
            List<Task> tasks = new List<Task>();

            Task first = factory.StartNew(() => 
            {
                Console.WriteLine("first");
                fandler.Handle(fold, file1);
            }, cts.Token);

            Task second = factory.StartNew(() =>
            {
                Console.WriteLine("second");
                fandler.Handle(fold, file2);
            }, cts.Token);

            tasks.Add(first);
            tasks.Add(second);

            Task.WaitAll(tasks.ToArray());
        }
    }
}
