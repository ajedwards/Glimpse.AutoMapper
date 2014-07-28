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
            : this(Mapper.Engine.ConfigurationProvider)
        {
        }

        public AutoMapperTab(IConfigurationProvider configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            this._configuration = configuration;
        }

        public override string Name
        {
            get
            {
                return "AutoMapper";
            }
        }

        public override object GetData(ITabContext context)
        {
            var plugin = Plugin.Create(Headers);

            if (context != null)
            {
                TypeMap[] typeMaps = this._configuration.GetAllTypeMaps();

                foreach (var profileName in typeMaps.Select(map => map.Profile).Distinct())
                {
                    var typeMapSection = new TypeMapTabSection(this._configuration, profileName);

                    plugin.AddRow().Column(profileName).Column(typeMapSection);
                }
            }

            return plugin;
        }
    }
}