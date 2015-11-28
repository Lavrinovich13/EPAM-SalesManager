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
using BL.ConfigValidator;
using log4net;

namespace ConsoleSalesManager
{
    class Program
    {
        protected static TaskFactory _taskFactory;
        protected static FileHandler _fileHandler;

        protected static string _serverFolderPath;
        protected static string _wrongFilesFolderPath;
        protected static string _processedFiles;

        protected static ILog _logger;

        static void Main(string[] args)
        {
            ApplicationConfigValidator validator = new ApplicationConfigValidator();

            log4net.Config.XmlConfigurator.Configure();
            _logger = LogManager.GetLogger("Console application");

            try
            {
                validator.Validate();

                _serverFolderPath = ConfigurationManager.AppSettings["ServerFolder"];
                _wrongFilesFolderPath = ConfigurationManager.AppSettings["NotAppropriateFilesFolder"];
                _processedFiles = ConfigurationManager.AppSettings["ProcessedFilesFolder"];

                var taskScheduler =
                    new LimitedTaskScheduler(Int32.Parse(ConfigurationManager.AppSettings["MaxDatabaseConnections"]));
                _taskFactory = new TaskFactory(taskScheduler);
                _fileHandler = new FileHandler();
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
                return;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
                return;
            }

            ScanServerFolder();

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = _serverFolderPath;
            watcher.Filter = "*.csv";
            watcher.Created += new FileSystemEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;

            Console.Read();
        }

        private static void ScanServerFolder()
        {
            var files = Directory.GetFiles(_serverFolderPath);
            foreach (var file in files)
            {
                var fileName = file.Substring(file.LastIndexOf("\\") + 1);
                OnChanged(null, new FileSystemEventArgs(WatcherChangeTypes.Created, _serverFolderPath, fileName));
            }
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            _taskFactory.StartNew(() =>
            {
                try
                {
                    _fileHandler.Handle(_serverFolderPath, e.Name);

                    //in transaction?
                    File.Move(
                        Path.Combine(_serverFolderPath, e.Name),
                        Path.Combine(_processedFiles, e.Name));
                }
                catch (Exception ex)
                {
                    using (log4net.NDC.Push("Console application"))
                    {
                        Console.WriteLine("Eroor occurs with file " + e.Name + ". " + ex.Message);
                        _logger.Warn("Eroor occurs with file by path " + e.FullPath + ". " + ex.Message);
                    }

                    File.Move(
                         Path.Combine(_serverFolderPath, e.Name),
                         Path.Combine(_wrongFilesFolderPath, e.Name));
                }
            });
        }
    }
}
