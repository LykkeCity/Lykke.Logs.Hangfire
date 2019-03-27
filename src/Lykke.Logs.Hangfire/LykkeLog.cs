using System;
using JetBrains.Annotations;
using Lykke.Common.Log;

using IHangfireLog = Hangfire.Logging.ILog;
using ILykkeLog = Common.Log.ILog;

using HangfireLogLevel = Hangfire.Logging.LogLevel;
using LykkeLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Lykke.Logs.Hangfire
{
    [PublicAPI]
    public class LykkeLog : IHangfireLog
    {
        private readonly ILykkeLog _log;
        
        internal LykkeLog(
            ILykkeLog log)
        {
            _log = log;
        }

        public bool Log(
            HangfireLogLevel logLevel,
            Func<string> messageFunc,
            Exception exception = null)
        {
            var lykkeLogLevel = MapLogLevel(logLevel);

            if (!_log.IsEnabled(lykkeLogLevel))
            {
                return false;
            }

            if (messageFunc != null)
            {
                var message = messageFunc();

                switch (lykkeLogLevel)
                {
                    case LykkeLogLevel.Trace:
                        _log.Trace(message, exception: exception);
                        break;
                    case LykkeLogLevel.Debug:
                        _log.Debug(message, exception: exception);
                        break;
                    case LykkeLogLevel.Information:
                        _log.Info(message, exception: exception);
                        break;
                    case LykkeLogLevel.Warning:
                        _log.Warning(message, exception: exception);
                        break;
                    case LykkeLogLevel.Error:
                        _log.Error(message, exception: exception);
                        break;
                    case LykkeLogLevel.Critical:
                        _log.Critical(message, exception: exception);
                        break;
                    case LykkeLogLevel.None:
                        break;
                    default:
                        throw new InvalidOperationException($"Log level [{logLevel}] has been mapped to the unsupported log level [{lykkeLogLevel}].");
                }
            }

            return true;
        }

        private static LykkeLogLevel MapLogLevel(
            HangfireLogLevel logLevel)
        {
            switch (logLevel)
            {
                case HangfireLogLevel.Trace:
                    return LykkeLogLevel.Trace;
                case HangfireLogLevel.Debug:
                    return LykkeLogLevel.Debug;
                case HangfireLogLevel.Info:
                    return LykkeLogLevel.Information;
                case HangfireLogLevel.Warn:
                    return LykkeLogLevel.Warning;
                case HangfireLogLevel.Error:
                    return LykkeLogLevel.Error;
                case HangfireLogLevel.Fatal:
                    return LykkeLogLevel.Critical;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}
