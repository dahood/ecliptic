using System;
using System.Diagnostics;
using EclipticLib.Utility.Logging.Providers;

namespace EclipticLib.Utility.Logging
{
    public static class Log
    {
        public static void Trace(Action<FormatMessageHandler> action)
        {
            GetLogger().Trace(action);
        }

        public static void Debug(Action<FormatMessageHandler> action)
        {
            GetLogger().Debug(action);
        }

        public static void Info(Action<FormatMessageHandler> action)
        {
            GetLogger().Debug(action);
        }

        public static void Warn(Action<FormatMessageHandler> action)
        {
            GetLogger().Warn(action);
        }

        public static void Error(Action<FormatMessageHandler> action)
        {
            GetLogger().Error(action);
        }

        public static void Fatal(Action<FormatMessageHandler> action)
        {
            GetLogger().Fatal(action);
        }

        private static ILogProvider GetLogger()
        {
            try
            {
                return For(CallingType());
            }
            catch
            {
                return new NullLogProvider();
            }
        }

        private static ILogProvider For<T>(T type)
        {
            return LogFactory.CreateFor<T>();
        }

        private static Type CallingType()
        {
            return new StackFrame(3).GetMethod().DeclaringType;
        }
    }
}