using System;
using Autofac;
using Hangfire;
using JetBrains.Annotations;
using Lykke.Common.Log;

namespace Lykke.Logs.Hangfire
{
    public static class GlobalConfigurationExtension
    {
        [PublicAPI]
        public static IGlobalConfiguration<LykkeLogProvider> UseLykkeLogProvider(
            [NotNull] this IGlobalConfiguration configuration,
            [NotNull] IContainer container)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof (configuration));
            }

            var logFactory = container.Resolve<ILogFactory>();
            var logProvider = new LykkeLogProvider(logFactory);
            
            return configuration.UseLogProvider(logProvider);
        }
    }
}
