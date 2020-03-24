using log4net;
using System;
using System.IO;
using System.Reflection;

namespace ee.Core.Logging
{
    public static class Logger
    {
        private static readonly object _locker = new object();
        private static ILog _defaultLogger;

        public static ILog Default
        {
            get
            {
                if (_defaultLogger == null)
                {
                    lock (_locker)
                    {
                        if (_defaultLogger == null)
                        {
                            _defaultLogger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), "SystemLog");
                        }
                    }
                }

                return _defaultLogger;
            }
        }



        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public static bool Configure(string program)
        {
            log4net.GlobalContext.Properties["program"] = program;

            var assembly = Assembly.GetExecutingAssembly();
            var repository = log4net.LogManager.CreateRepository(assembly, typeof(log4net.Repository.Hierarchy.Hierarchy));
            var file = new System.IO.FileInfo(Path.Combine(AssemblyDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(repository, file);

            var repo = log4net.LogManager.GetRepository(assembly);

            if (repo.Configured)
            {
                _defaultLogger = LogManager.GetLogger(assembly, "Default");
            }
            return repo.Configured;
        }


        public static ILog GetLogger(string loggerName)
        {
            if (string.IsNullOrEmpty(loggerName) || loggerName == "Default")
            {
                return Default;
            }
            else
            {
                return LogManager.GetLogger(Assembly.GetExecutingAssembly(), loggerName);
            }
        }

        public static void Debug(object message, string loggerName = "")
        {
            GetLogger(loggerName)?.Debug(message);
        }
        public static void Debug(object message, Exception exception, string loggerName = "")
        {
            GetLogger(loggerName)?.Debug(message, exception);
        }

        public static void Info(object message, string loggerName = "")
        {
            GetLogger(loggerName)?.Info(message);
        }

        public static void Info(object message, Exception exception, string loggerName = "")
        {
            GetLogger(loggerName)?.Info(message, exception);
        }

        public static void Warn(object message, string loggerName = "")
        {
            GetLogger(loggerName)?.Warn(message);
        }

        public static void Warn(object message, Exception exception, string loggerName = "")
        {
            GetLogger(loggerName)?.Warn(message, exception);
        }


        public static void Error(object message, string loggerName = "")
        {
            GetLogger(loggerName)?.Error(message);
        }
        public static void Error(object message, Exception exception, string loggerName = "")
        {
            GetLogger(loggerName)?.Error(message, exception);
        }

        public static void Fatal(object message, string loggerName = "")
        {
            GetLogger(loggerName)?.Fatal(message);
        }

        public static void Fatal(object message, Exception exception, string loggerName = "")
        {
            GetLogger(loggerName)?.Fatal(message, exception);
        }


    }
}
