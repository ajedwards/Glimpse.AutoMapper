using System;
using System.Linq;

using AutoMapper;

using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;

namespace Glimpse.AutoMapper
{
    public class AutoMapperTab : TabBase
    {
        private static readonly string[] Headers = { "Profile", "Type Map" };

        private readonly IConfigurationProvider _configuration;

        public AutoMapperTab()
            : this(Mapper.Configuration)
        {
        }

        public AutoMapperTab(IConfigurationProvider configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public override string Name => "AutoMapper";

        public override object GetData(ITabContext context)
        {
            var plugin = Plugin.Create(Headers);

            TypeMap[] typeMaps = this._configuration.GetAllTypeMaps();
            foreach (var profileName in typeMaps.Select(map => map.Profile.Name).Distinct())
            {
                var typeMapSection = new TypeMapTabSection(this._configuration, profileName);
                plugin.AddRow().Column(profileName).Column(typeMapSection);
            }

            return plugin;
        }
    }
}