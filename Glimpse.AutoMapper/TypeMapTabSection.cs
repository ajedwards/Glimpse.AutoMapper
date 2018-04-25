using System;
using System.Linq;

using AutoMapper;

using Glimpse.Core.Tab.Assist;

namespace Glimpse.AutoMapper
{
    public class TypeMapTabSection : TabSection
    {
        private static readonly string[] Headers = { "Source Type", "Destination Type", "Destination Type Override" };

        public TypeMapTabSection(IConfigurationProvider configuration, string profileName)
            : base(Headers)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            foreach (var map in configuration.GetAllTypeMaps().Where(map => map.Profile.Name == profileName))
            {
                this.AddRow()
                    .Column(map.SourceType != null ? map.SourceType.FullName : null)
                    .Column(map.DestinationType != null ? map.DestinationType.FullName : null)
                    .Column(map.DestinationTypeOverride != null ? map.DestinationTypeOverride.FullName : null);
            }
        }
    }
}
