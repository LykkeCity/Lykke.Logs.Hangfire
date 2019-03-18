using Autofac;
using Hangfire.Logging;
using JetBrains.Annotations;
using Lykke.Common.Log;

namespace Lykke.Logs.Hangfire
{
    [PublicAPI]
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterHangfireLogsAdapter(
            this ContainerBuilder builder)
        {
            builder
                .RegisterBuildCallback(container =>
                {
                    var logFactory = container.Resolve<ILogFactory>();
                    var logProvider = new LykkeLogProvider(logFactory);
                    
                    LogProvider
                        .SetCurrentLogProvider(logProvider);
                });

            return builder;
        }
    }
}