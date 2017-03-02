using EclipticLib.Utility.Logging.Providers;
using NLog;

namespace EclipticLib.Utility.Logging
{
    public static class LogFactory
    {
        internal static ILogProvider CreateFor<T>()
        {
            return new NLogProvider(LogManager.GetLogger(typeof(T).Name));
        }
    }
}