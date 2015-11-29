﻿using BL;
using BL.ConfigValidator;
using BL.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SalesManagerService
{
    public partial class SalesManagerService : ServiceBase
    {
        protected static TaskFactory _taskFactory;
        protected static FileHandler _fileHandler;

        protected static string _serverFolderPath;
        protected static string _wrongFilesFolderPath;
        protected static string _processedFiles;
        protected static CancellationTokenSource _cancelationTokenSource;

        public SalesManagerService()
        {
            InitializeComponent();

            ApplicationConfigValidator validator = new ApplicationConfigValidator();
            try
            {
                validator.Validate();

                _serverFolderPath = ConfigurationManager.AppSettings["ServerFolder"];
                _wrongFilesFolderPath = ConfigurationManager.AppSettings["NotAppropriateFilesFolder"];
                _processedFiles = ConfigurationManager.AppSettings["ProcessedFilesFolder"];

                _cancelationTokenSource = new CancellationTokenSource();

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
        }

        protected override void OnStart(string[] args)
        {
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

                   File.Move(
                       Path.Combine(_serverFolderPath, e.Name),
                       Path.Combine(_processedFiles, e.Name));
               }
               catch (Exception ex)
               {
                   if (File.Exists(Path.Combine(_wrongFilesFolderPath, e.Name)))
                       File.Delete(Path.Combine(_wrongFilesFolderPath, e.Name));

                   File.Move(
                        Path.Combine(_serverFolderPath, e.Name),
                        Path.Combine(_wrongFilesFolderPath, e.Name));
               }
           }, _cancelationTokenSource.Token);
        }

        protected override void OnStop()
        {
            _cancelationTokenSource.Cancel();
            _cancelationTokenSource.Dispose();
        }
    }
}
