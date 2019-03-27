using System;
using Hangfire.Logging;
using JetBrains.Annotations;
using Lykke.Common.Log;

namespace Lykke.Logs.Hangfire
{
    [PublicAPI]
    public class LykkeLogProvider : ILogProvider
    {
        private readonly ILogFactory _logFactory;

        
        public LykkeLogProvider(
            ILogFactory logFactory)
        {
            _logFactory = logFactory ?? throw new ArgumentNullException(nameof(logFactory));
        }

        
        public ILog GetLogger(
            string name)
        {
            var log = _logFactory.CreateLog(this, name);
            
            return new LykkeLog(log);
        }
    }
}