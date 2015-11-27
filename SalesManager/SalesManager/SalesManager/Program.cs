using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BL.Scheduler;
using System.Threading;
using BL;
using BL.Parser;
using BL.Reader;
using System.Configuration;

namespace ConsoleSalesManager
{
    class Program
    {
        protected static TaskFactory _taskFactory;
        protected static FileHandler _fileHandler;

        static void Main(string[] args)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = ConfigurationManager.AppSettings["FolderPath"];
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.csv";
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            var taskScheduler = new LimitedTaskScheduler(Int32.Parse(ConfigurationManager.AppSettings["MaxDatabaseConnections"]));
            _taskFactory = new TaskFactory(taskScheduler);

            _fileHandler = new FileHandler();

            Console.Read();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            _taskFactory.StartNew(() =>
            {
                _fileHandler.Handle(ConfigurationManager.AppSettings["FolderPath"], e.Name);
            });
        }
    }
}
