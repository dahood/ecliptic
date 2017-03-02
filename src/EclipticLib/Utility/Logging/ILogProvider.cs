using System;

namespace EclipticLib.Utility.Logging
{
    public delegate void FormatMessageHandler(string format, params object[] args);

    public delegate void FormatExceptionMessageHandler(Exception ex, string message = null, params object[] args);

    public interface ILogProvider
    {
        void Trace(Action<FormatMessageHandler> action);
        void Debug(Action<FormatMessageHandler> action);
        void Info(Action<FormatMessageHandler> action);
        void Warn(Action<FormatMessageHandler> action);
        void Error(Action<FormatMessageHandler> action);
        void Fatal(Action<FormatMessageHandler> action);

        void Trace(Action<FormatExceptionMessageHandler> action);
        void Debug(Action<FormatExceptionMessageHandler> action);
        void Info(Action<FormatExceptionMessageHandler> action);
        void Warn(Action<FormatExceptionMessageHandler> action);
        void Error(Action<FormatExceptionMessageHandler> action);
        void Fatal(Action<FormatExceptionMessageHandler> action);
    }
}