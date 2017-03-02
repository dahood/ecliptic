using System;
using EclipticLib.Extensions;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace EclipticLib.Utility.Logging.Providers
{
    /// <summary>
    /// Full support for NLog so it can be plugged in
    /// </summary>
    public class NLogProvider : ILogProvider
    {
        private readonly Logger logger;

        public NLogProvider()
        {
            logger = CreateLogger();
        }

        public NLogProvider(Logger logger)
        {
            this.logger = logger;
        }

        public bool TraceEnabled => logger.IsTraceEnabled;
        public bool DebugEnabled => logger.IsDebugEnabled;
        public bool InfoEnabled => logger.IsInfoEnabled;
        public bool WarnEnabled => logger.IsWarnEnabled;
        public bool ErrorEnabled => logger.IsErrorEnabled;
        public bool FatalEnabled => logger.IsFatalEnabled;

        public void Trace(Action<FormatMessageHandler> action)
        {
            if (!logger.IsTraceEnabled) return;
            action.Invoke(Trace);
        }

        public void Debug(Action<FormatMessageHandler> action)
        {
            if (!logger.IsDebugEnabled) return;
            action.Invoke(Debug);
        }

        public void Info(Action<FormatMessageHandler> action)
        {
            if (!logger.IsInfoEnabled) return;
            action.Invoke(Info);
        }

        public void Warn(Action<FormatMessageHandler> action)
        {
            if (!logger.IsWarnEnabled) return;
            action.Invoke(Warn);
        }

        public void Error(Action<FormatMessageHandler> action)
        {
            if (!logger.IsErrorEnabled) return;
            action.Invoke(Error);
        }

        public void Fatal(Action<FormatMessageHandler> action)
        {
            if (!logger.IsFatalEnabled) return;
            action.Invoke(Fatal);
        }

        public void Trace(Action<FormatExceptionMessageHandler> action)
        {
            if (!logger.IsTraceEnabled) return;
            action.Invoke(Trace);
        }

        public void Debug(Action<FormatExceptionMessageHandler> action)
        {
            if (!logger.IsDebugEnabled) return;
            action.Invoke(Debug);
        }

        public void Info(Action<FormatExceptionMessageHandler> action)
        {
            if (!logger.IsInfoEnabled) return;
            action.Invoke(Info);
        }

        public void Warn(Action<FormatExceptionMessageHandler> action)
        {
            if (!logger.IsWarnEnabled) return;
            action.Invoke(Warn);
        }

        public void Error(Action<FormatExceptionMessageHandler> action)
        {
            if (!logger.IsErrorEnabled) return;
            action.Invoke(Error);
        }

        public void Fatal(Action<FormatExceptionMessageHandler> action)
        {
            if (!logger.IsFatalEnabled) return;
            action.Invoke(Fatal);
        }

        public void Trace(string message, params object[] args)
        {
            logger.Trace(message.FormatWith(args));
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message.FormatWith(args));
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message.FormatWith(args));
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message.FormatWith(args));
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message.FormatWith(args));
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message.FormatWith(args));
        }

        public void Trace(Exception ex, string message = null, params object[] args)
        {
            logger.Trace(ex, message == null ? null : string.Format(message, args));
        }

        public void Debug(Exception ex, string message = null, params object[] args)
        {
            logger.Debug(ex, message == null ? null : string.Format(message, args));
        }

        public void Info(Exception ex, string message = null, params object[] args)
        {
            logger.Info(ex, message == null ? null : string.Format(message, args));
        }

        public void Warn(Exception ex, string message = null, params object[] args)
        {
            logger.Warn(ex, message == null ? null : string.Format(message, args));
        }

        public void Error(Exception ex, string message = null, params object[] args)
        {
            logger.Error(ex, message == null ? null : string.Format(message, args));
        }

        public void Fatal(Exception ex, string message = null, params object[] args)
        {
            logger.Fatal(ex, message == null ? null : string.Format(message, args));
        }


        private Logger CreateLogger()
        {
            var config = CreateDefaultLoggingConfig();
            LogManager.Configuration = config;

            return LogManager.GetCurrentClassLogger();
        }

        private static LoggingConfiguration CreateDefaultLoggingConfig()
        {
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties 
            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";
            fileTarget.FileName = "${basedir}/file.txt";
            fileTarget.Layout = "${message}";

            // Step 4. Define rules
            var rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);

            var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);
            return config;
        }
    }
}