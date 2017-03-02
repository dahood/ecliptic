using System;

namespace EclipticLib.Utility.Logging.Providers
{
    public class NullLogProvider : ILogProvider
    {
        public void Trace(Action<FormatMessageHandler> action)
        {
        }

        public void Debug(Action<FormatMessageHandler> action)
        {
        }

        public void Info(Action<FormatMessageHandler> action)
        {
        }

        public void Warn(Action<FormatMessageHandler> action)
        {
        }

        public void Error(Action<FormatMessageHandler> action)
        {
        }

        public void Fatal(Action<FormatMessageHandler> action)
        {
        }

        public void Trace(Action<FormatExceptionMessageHandler> action)
        {
        }

        public void Debug(Action<FormatExceptionMessageHandler> action)
        {
        }

        public void Info(Action<FormatExceptionMessageHandler> action)
        {
        }

        public void Warn(Action<FormatExceptionMessageHandler> action)
        {
        }

        public void Error(Action<FormatExceptionMessageHandler> action)
        {
        }

        public void Fatal(Action<FormatExceptionMessageHandler> action)
        {
        }

        public void Debug(string message)
        {
        }

        public void DebugFormat(string format, params object[] args)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, Exception exception)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

        public void Warn(string message, Exception exception)
        {
        }

        public void WarnFormat(string format, params object[] args)
        {
        }

        public void Error(string message, Exception exception)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void Fatal(string message, Exception exception)
        {
        }

        public void FatalFormat(string format, params object[] args)
        {
        }
    }
}