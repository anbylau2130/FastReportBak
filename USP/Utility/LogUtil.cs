using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Utility
{
    public class LogUtil
    {
        private static Hashtable monitors = new Hashtable();
        public static void Debug(string loggerName,string message)
        {
            if (!monitors.ContainsKey(loggerName))
            {
                monitors.Add(loggerName, new Object());
            }
            System.Threading.Monitor.Enter(monitors[loggerName]);
            try
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(loggerName);
                logger.Debug(message);
            }
            finally
            {
                System.Threading.Monitor.Exit(monitors[loggerName]);
            }
        }

        public static void Info(string loggerName, string message)
        {
            if (!monitors.ContainsKey(loggerName))
            {
                monitors.Add(loggerName, new Object());
            }
            System.Threading.Monitor.Enter(monitors[loggerName]);
            try
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(loggerName);
                logger.Info(message);
            }
            finally
            {
                System.Threading.Monitor.Exit(monitors[loggerName]);
            }
        }

        public static void Warn(string loggerName, string message)
        {
            if (!monitors.ContainsKey(loggerName))
            {
                monitors.Add(loggerName, new Object());
            }
            System.Threading.Monitor.Enter(monitors[loggerName]);
            try
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(loggerName);
                logger.Warn(message);
            }
            finally
            {
                System.Threading.Monitor.Exit(monitors[loggerName]);
            }
        }

        public static void Fatal(string loggerName, string message)
        {
            if (!monitors.ContainsKey(loggerName))
            {
                monitors.Add(loggerName, new Object());
            }
            System.Threading.Monitor.Enter(monitors[loggerName]);
            try
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(loggerName);
                logger.Fatal(message);
            }
            finally
            {
                System.Threading.Monitor.Exit(monitors[loggerName]);
            }
        }

        public static void Exception(string loggerName, Exception exception)
        {
            if (null != exception)
            {
                if (!monitors.ContainsKey(loggerName))
                {
                    monitors.Add(loggerName, new Object());
                }
                System.Threading.Monitor.Enter(monitors[loggerName]);
                try
                {
                    log4net.ILog logger = log4net.LogManager.GetLogger(loggerName);
                    if (exception.InnerException == null)
                    {
                        logger.Error(string.Format("System Exception:{0}{1}{2}",
                            exception.Message,
                            System.Environment.NewLine,
                            exception.StackTrace
                            ));
                    }
                    else
                    {
                        logger.Error(string.Format("System Exception:{0}{1}{2}{3}InnerException:{4}{5}{6}",
                        exception.Message,
                        System.Environment.NewLine,
                        exception.StackTrace,
                        System.Environment.NewLine,
                        exception.InnerException.Message,
                        System.Environment.NewLine,
                        exception.InnerException.StackTrace));
                    }
                }
                finally
                {
                    System.Threading.Monitor.Exit(monitors[loggerName]);
                }
            }
        }
    }
}